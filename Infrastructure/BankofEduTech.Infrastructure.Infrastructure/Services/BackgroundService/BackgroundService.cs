using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Infrastructure.Services.BackgroundService
{
    public class BackgroundService : IBackgroundEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public BackgroundService(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task SendEmailUpcomingPayment()
        {
            var payment = _unitOfWork.CustomerCreditInstallementsReadRepository.Table.Include(x => x.CustomerCredits).ThenInclude(x => x.AppUser).Where(x => x.PaymentDate.AddDays(-3) == DateTime.Now.Date && !x.IsPaid);

            foreach (var credit in payment)
            {
                EmailMessageModel message = new EmailMessageModel
                {
                    Body = $"KREDİNİZİN {credit.NumberOfInstallements}. TAKSİDİNİN ÖDEMESİ {credit.AmountOfInstallements} TL'dir. SON ÖDEME TARİHİNİZE 3 GÜN KALDI. LÜTFEN ÖDEME YAPINIZ. ÖDEME YAPTIYSANIZ BU MAİLİ DİKKATE ALMAYINIZ.",
                    From = "omerolmez.012@gmail.com",
                    Subject = "KREDİ ÖDEME HATIRLATMASI",
                    To = credit.CustomerCredits.AppUser.Email
                };
                await _emailSender.SendMailAsync(message);
            }

            throw new NotImplementedException();
        }
    }
}
