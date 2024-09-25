using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories.AppUser;
using BankofEduTech.Core.Application.Repositories.CustomerAccount;
using BankofEduTech.Core.Application.Repositories.CustomerAccountActivities;
using BankofEduTech.Core.Application.Repositories.CustomerCredits;
using BankofEduTech.Core.Application.Repositories.CustomerCreditsInstallements;
using BankofEduTech.Core.Application.Repositories.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IAppUserReadRepository AppUserReadRepository { get; }
		IAppUserWriteRepository AppUserWriteRepository { get; }
		ICustomerAccountReadRepository CustomerAccountReadRepository { get; }
		ICustomerAccountWriteRepository CustomerAccountWriteRepository { get; }
		ICustomerAccountActivitiesReadRepository CustomerAccountActivitiesReadRepository { get; }
		ICustomerAccountActivitiesWriteRepository CustomerAccountActivitiesWriteRepository { get; }
		ICustomerCreditWriteRepository CustomerCreditWriteRepository { get; }
		ICustomerCreditReadRepository CustomerCreditReadRepository { get; }
		ICustomerCreditInstallementsReadRepository CustomerCreditInstallementsReadRepository { get; }
		ICustomerCreditInstallementsWriteRepository CustomerCreditInstallementsWriteRepository { get; }
		IGenerateCodeService GenerateCodeService { get; }
        INotificationWriteRepository NotificationWriteRepository { get; }
        INotificationReadRepository NotificationReadRepository { get; }
    }
}
