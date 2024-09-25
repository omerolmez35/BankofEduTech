using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Abstractions
{
    public interface IEmailSender
    {
        Task SendMailAsync(EmailMessageModel model);
    }
}
