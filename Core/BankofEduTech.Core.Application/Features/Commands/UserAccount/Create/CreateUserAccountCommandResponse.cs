
using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Core.Domain.Entities;

namespace BankofEduTech.Core.Application.Features.Commands.UserAccount.Create
{
    public class CreateUserAccountCommandResponse : ResultModel
    {
        public int AccountID { get; set; }
        public string Currency { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
    }
}
