using BankofEduTech.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCreditPayments
{
    public class GetCreditPaymentsQueryResponse : ResultModel
    {
        public int NumberOfInstallements { get; set; }
        public string AmountOfInstallements { get; set; }
        public string AmountOfInterest { get; set; }
        public string AmountOfPrincipal { get; set; }
        public string RestOfPrincipal { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsPaid { get; set; }
        public int InstallementsID { get; set; }

    }
}
