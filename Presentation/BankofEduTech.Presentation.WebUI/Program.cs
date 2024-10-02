using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using BankofEduTech.Core.Application;
using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Presentation.WebUI.Services.SignalRService;
using BankofEduTech.Presentation.WebUI.SignalRHubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankofEduTech.Presentation.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
            builder.Services.AddScoped<ClaimService>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(NotificationHub).Assembly); 
            });
            builder.Services.AddScoped<ISignalRService, SignalRService>();
            builder.Services.AddScoped<NotificationHub>();
            builder.Services.AddSignalR();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                            policy.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true)
                                  )); 
            builder.Services.AddHttpClient();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,



        };


    });
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
  
            app.MapHub<NotificationHub>("/notificationhub");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNotyf();

            app.UseRouting();
            app.UseSession();
            app.Use(async (httpContext, next) =>
            {
                var JWToken = httpContext.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    httpContext.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaDefault",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");
            });


            app.Run();
        }
    }
}