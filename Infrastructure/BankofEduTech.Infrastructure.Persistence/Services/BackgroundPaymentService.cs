using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Repositories;
using BankofEduTech.Core.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.StaticServices
{
   
    public class BackgroundPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public BackgroundPaymentService(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        public async Task SendEmailUpcomingPayment()
        {
            try
            {
                var payment = _unitOfWork.CustomerCreditInstallementsReadRepository.Table.Include(x => x.CustomerCredits).ThenInclude(x => x.AppUser).Where(x => x.ExpireDate.Date  == DateTime.Now.Date.AddDays(3) && !x.IsPaid).ToList();
                if (payment != null && 0 < payment.Count)
                {
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
                }
             
            }
            catch (Exception ex)
            {

                throw;
            }
          

  
        }

    }
}
