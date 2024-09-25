using BankofEduTech.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId
{
    public class GetApplicationByUserIdQueryResponse
    {

        public decimal AmountOfLoan { get; set; }
        public decimal TotalBackPaymnent { get; set; }
        public int CountofInstallements { get; set; }
        public decimal InterestRate { get; set; }
        public int CreditID { get; set; }
        public CreditStatus CreditStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
