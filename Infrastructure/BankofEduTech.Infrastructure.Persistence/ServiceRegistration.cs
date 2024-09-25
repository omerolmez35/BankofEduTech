using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Application.Repositories.AppUser;
using BankofEduTech.Core.Application.Repositories.CustomerAccount;
using BankofEduTech.Core.Application.Repositories.CustomerAccountActivities;
using BankofEduTech.Core.Application.Repositories.CustomerCredits;
using BankofEduTech.Core.Application.Repositories.CustomerCreditsInstallements;
using BankofEduTech.Core.Application.Repositories.Notifications;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Infrastructure.Persistence.Context;
using BankofEduTech.Infrastructure.Persistence.Repositories;
using BankofEduTech.Infrastructure.Persistence.Repositories.AppUser;
using BankofEduTech.Infrastructure.Persistence.Repositories.CustomerAccount;
using BankofEduTech.Infrastructure.Persistence.Repositories.CustomerAccountActivities;
using BankofEduTech.Infrastructure.Persistence.Repositories.CustomerCredits;
using BankofEduTech.Infrastructure.Persistence.Repositories.CustomerCreditsInstallements;
using BankofEduTech.Infrastructure.Persistence.Repositories.Notifications;
using BankofEduTech.Infrastructure.Persistence.Services.GenerateCodeService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankofEduTech.Infrastructure.Persistence
{
    public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<BankofEduTechContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Add the custom repository to the service container
			services.AddScoped<IAppUserWriteRepository, AppUserWriteRepository>();
			services.AddScoped<IAppUserReadRepository, AppUserReadRepository>();
			services.AddScoped<ICustomerAccountReadRepository, CustomerAccountReadRepository>();
			services.AddScoped<ICustomerAccountWriteRepository, CustomerAccountWriteRepository>();
			services.AddScoped<ICustomerAccountActivitiesWriteRepository, CustomerAccountActivitiesWriteRepository>();
			services.AddScoped<ICustomerAccountActivitiesReadRepository, CustomerAccountActivitiesReadRepository>();
			services.AddScoped<ICustomerCreditReadRepository, CustomerCreditsReadRepository>();
			services.AddScoped<ICustomerCreditWriteRepository, CustomerCreditsWriteRepository>();
			services.AddScoped<ICustomerCreditInstallementsReadRepository, CustomerCreditsInstallementsReadRepository>();
			services.AddScoped<ICustomerCreditInstallementsWriteRepository, CustomerCreditsInstallementsWriteRepository>();
            services.AddScoped<INotificationReadRepository, NotificationReadRepository>();
            services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();
            services.AddScoped<IGenerateCodeService, GenerateCodeService>();
         




        }
    }
}
