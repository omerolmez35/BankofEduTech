using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Application.Repositories.AppUser;
using BankofEduTech.Core.Application.Repositories.CustomerAccount;
using BankofEduTech.Core.Application.Repositories.CustomerAccountActivities;
using BankofEduTech.Core.Application.Repositories.CustomerCredits;
using BankofEduTech.Core.Application.Repositories.CustomerCreditsInstallements;
using BankofEduTech.Core.Application.Repositories.Notifications;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankofEduTechContext _context;
        public IAppUserWriteRepository AppUserWriteRepository { get; }
        public IAppUserReadRepository AppUserReadRepository { get; }

        public ICustomerAccountReadRepository CustomerAccountReadRepository { get; }

        public ICustomerAccountWriteRepository CustomerAccountWriteRepository { get; }

        public ICustomerAccountActivitiesReadRepository CustomerAccountActivitiesReadRepository { get; }

        public ICustomerAccountActivitiesWriteRepository CustomerAccountActivitiesWriteRepository { get; }
        public ICustomerCreditReadRepository CustomerCreditReadRepository { get; }
        public ICustomerCreditWriteRepository CustomerCreditWriteRepository { get; }
        public ICustomerCreditInstallementsReadRepository CustomerCreditInstallementsReadRepository { get; }
        public ICustomerCreditInstallementsWriteRepository CustomerCreditInstallementsWriteRepository { get; }

        public IGenerateCodeService GenerateCodeService { get; }

        public INotificationWriteRepository NotificationWriteRepository { get; }

        public INotificationReadRepository NotificationReadRepository { get; }

        public UnitOfWork(BankofEduTechContext context, IAppUserWriteRepository appUserWriteRepository, IAppUserReadRepository appUserReadRepository, ICustomerAccountReadRepository customerAccountReadRepository, ICustomerAccountWriteRepository customerAccountWriteRepository, ICustomerAccountActivitiesReadRepository customerAccountActivitiesReadRepository, ICustomerAccountActivitiesWriteRepository customerAccountActivitiesWriteRepository, ICustomerCreditReadRepository customerCreditReadRepository, ICustomerCreditWriteRepository customerCreditWriteRepository, ICustomerCreditInstallementsReadRepository customerCreditInstallementsReadRepository, ICustomerCreditInstallementsWriteRepository customerCreditInstallementsWriteRepository, IGenerateCodeService generateCodeService, INotificationWriteRepository notificationWriteRepository, INotificationReadRepository notificationReadRepository)
        {
            _context = context;
            AppUserWriteRepository = appUserWriteRepository;
            AppUserReadRepository = appUserReadRepository;
            CustomerAccountReadRepository = customerAccountReadRepository;
            CustomerAccountWriteRepository = customerAccountWriteRepository;
            CustomerAccountActivitiesReadRepository = customerAccountActivitiesReadRepository;
            CustomerAccountActivitiesWriteRepository = customerAccountActivitiesWriteRepository;
            CustomerCreditReadRepository = customerCreditReadRepository;
            CustomerCreditWriteRepository = customerCreditWriteRepository;
            CustomerCreditInstallementsReadRepository = customerCreditInstallementsReadRepository;
            CustomerCreditInstallementsWriteRepository = customerCreditInstallementsWriteRepository;
            GenerateCodeService = generateCodeService;
            NotificationWriteRepository = notificationWriteRepository;
            NotificationReadRepository = notificationReadRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
