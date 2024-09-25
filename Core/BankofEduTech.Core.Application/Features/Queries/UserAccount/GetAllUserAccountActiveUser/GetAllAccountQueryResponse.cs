using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser
{
    public class GetAllAccountQueryResponse : ResultModel
    {
        public string AccountName { get; set; }
        public int AccountID { get; set; }
        public string AccountType { get; set; }
        public string AccountCurrency { get; set; }
        public string AccountBalance { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
