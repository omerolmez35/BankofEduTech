using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.Customer.GetAllCustomer;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.ViewModels
{
    public class CustomerDetailViewModel
    {
        public GetUserByNameQueryResponse AccountInfo { get; set; }
        public List<GetAllAccountQueryResponse> AllAccount { get; set; }
        public List<GetAllAccountQueryResponse> AllCreditAccount { get; set; }
        public List<GetApplicationByUserIdQueryResponse> AllApplication { get; set; }
    }
}
