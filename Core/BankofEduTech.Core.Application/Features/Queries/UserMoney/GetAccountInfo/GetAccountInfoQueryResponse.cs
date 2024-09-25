using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo
{
    public class GetAccountInfoQueryResponse : ResultModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int AccountID { get; set; }

    }
}
