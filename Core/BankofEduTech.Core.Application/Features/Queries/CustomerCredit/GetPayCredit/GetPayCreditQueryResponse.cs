using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId;
using BankofEduTech.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPayCredit
{
    public class GetPayCreditQueryResponse : ResultModel
    {
        public decimal Amount { get; set; }
        public decimal TotalAmount {
            get
            {
                return Amount + Convert.ToDecimal(DelayInterest);
            }
             }
        public int NumberOfInstallements { get; set; }
        public string DelayInterest { get; set; }
        public List<GetAllAccountForByUserIdQueryResponse> AllAccount { get; set; }
    }
}
