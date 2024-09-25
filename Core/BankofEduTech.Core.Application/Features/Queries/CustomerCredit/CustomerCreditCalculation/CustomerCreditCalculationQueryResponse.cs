using BankofEduTech.Core.Application.ViewModels;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation
{
    public class CustomerCreditCalculationQueryResponse: ResultModel
    {
        public int NumberOfInstallements { get; set; }
        public string AmountOfInstallements { get; set; }
        public string AmountOfInterest { get; set; }
        public string AmountOfPrincipal { get; set; }
        public string RestOfPrincipal { get; set; }
        public string ExpireDate { get; set; }
    }
}
