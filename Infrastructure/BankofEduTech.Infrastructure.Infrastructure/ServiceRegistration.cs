using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Infrastructure.Infrastructure.Services.EmailService;
using BankofEduTech.Infrastructure.Infrastructure.Services.SeedService;
using BankofEduTech.Infrastructure.Infrastructure.Services.TokenService;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankofEduTech.Infrastructure.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISeedService, SeedService>();

            services.AddHangfire(config =>
      config.UseSqlServerStorage("Server=127.0.0.1,1433; uid = sa; pwd = Omer1234;Database=BankofEduTechDB;Trusted_Connection=False;Trust Server Certificate=True;"));


            services.AddHangfireServer();



        }
    }
}
