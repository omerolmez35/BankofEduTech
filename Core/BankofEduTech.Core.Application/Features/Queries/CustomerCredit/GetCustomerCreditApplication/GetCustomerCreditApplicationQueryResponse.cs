using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCustomerCreditApplication
{
    public class GetCustomerCreditApplicationQueryResponse
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal AmountOfLoan { get; set; }
        public decimal TotalBackPaymnent { get; set; }
        public int CountofInstallements { get; set; }
        public decimal InterestRate { get; set; }
        public int CreditID { get; set; }
        public Guid UserID { get; set; }

    }
}
