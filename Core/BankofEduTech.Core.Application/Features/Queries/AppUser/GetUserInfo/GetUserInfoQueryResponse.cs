using BankofEduTech.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserInfo
{
    public class GetUserInfoQueryResponse: ResultModel
    {
        public decimal TotalBalance { get; set; }
        public decimal TotalDebt { get; set; }
        public int TotalAccount { get; set; }
        public int LatePayment { get; set; }
    }
}
