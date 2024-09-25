using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId
{
    public class GetAllAccountForByUserIdQueryResponse : ResultModel
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public int AccountID { get; set; }
    }
}
