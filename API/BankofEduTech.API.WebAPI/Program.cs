using System.Security.Cryptography.X509Certificates;
using BankofEduTech.Core.Application;
using BankofEduTech.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using BankofEduTech.Infrastructure.Persistence.Context;
using BankofEduTech.Infrastructure.Persistence;
using BankofEduTech.Core.Application.Features.Commands.AppUser.Create;
using BankofEduTech.Core.Application.Mapping;
using BankofEduTech.API.WebAPI.IdentityValidator;
using BankofEduTech.Infrastructure.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BankofEduTech.Core.Application.Abstractions;
using Serilog;
using Serilog.Core;
using BankofEduTech.API.WebAPI.Middlewares;
using Hangfire;
using BankofEduTech.Core.Application.StaticServices;
namespace BankofEduTech.API.WebAPI
{
    public class Program
    {


        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddApplicationRegistration();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            // Add services to the container.
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                            policy.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true)
                                  ));
            Logger log = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            builder.Host.UseSerilog(log);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero

                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                //kullanıcının parola ayarları içindir
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 1;
                //options.User.AllowedUserNameCharacters = "abcdefgğhijklmnopqrstuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
            }).AddDefaultTokenProviders().AddRoles<AppRole>().AddEntityFrameworkStores<BankofEduTechContext>().AddErrorDescriber<CustomIdentityValidator>();


            var app = builder.Build();
            app.UseHangfireDashboard();

            // Recurring job eklemek
            RecurringJob.AddOrUpdate<BackgroundPaymentService>(
                "SendPaymentEmailService",
                service => service.SendEmailUpcomingPayment(),
                Cron.Daily);
            app.UseMiddleware<RequestLoggingMiddleware>();
            await SeedDatabaseAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
     

            app.MapControllers();

            await app.RunAsync();
        }

        static async Task SeedDatabaseAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var seedService = services.GetRequiredService<ISeedService>();
                    await seedService.SeedRolesAndAdminAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Hata oluştu.");
                }
            }
        }
    }
}
