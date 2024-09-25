using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities
{
    public class GetAccountActivitiesQueryResponse : ResultModel
    {
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal Amount { get; set; }
    }
}
