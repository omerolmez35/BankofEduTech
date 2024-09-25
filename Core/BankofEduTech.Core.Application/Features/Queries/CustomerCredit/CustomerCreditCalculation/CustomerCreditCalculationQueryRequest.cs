using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation
{
    public class CustomerCreditCalculationQueryRequest : IRequest<List<CustomerCreditCalculationQueryResponse>>
    {
        public double AmountOfLoan { get; set; }
        public double InterestRate { get; set; }
        public int NumberofInstallement { get; set; }

        public CustomerCreditCalculationQueryRequest(double amountOfLoan, double interestRate, int numberofInstallement)
        {
            AmountOfLoan = amountOfLoan;
            InterestRate = interestRate;
            NumberofInstallement = numberofInstallement;
        }
    }
}
