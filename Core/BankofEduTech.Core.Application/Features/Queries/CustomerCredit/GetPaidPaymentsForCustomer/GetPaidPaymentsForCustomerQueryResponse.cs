using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPaidPaymentsForCustomer
{
    public class GetPaidPaymentsForCustomerQueryResponse
    {
        public int NumberOfInstallements { get; set; }
        public string AmountOfInstallements { get; set; }
        public bool IsLate { get; set; }
    }
}
