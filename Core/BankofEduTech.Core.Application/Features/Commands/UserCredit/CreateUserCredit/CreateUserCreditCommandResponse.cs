using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Core.Domain.Entities;

namespace BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit
{
    public class CreateUserCreditCommandResponse : ResultModel
    {
        public Guid? UserID { get; set; }
        public CreditStatus CreditStatus { get; set; }
    }
}
