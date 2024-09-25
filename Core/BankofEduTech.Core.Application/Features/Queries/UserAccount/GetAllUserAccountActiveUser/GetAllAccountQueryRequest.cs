using BankofEduTech.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser
{
    public class GetAllAccountQueryRequest : IRequest<List<GetAllAccountQueryResponse>>
    {

        public AccountType AccountType { get; set; }
        public Guid UserID { get; set; }

        public GetAllAccountQueryRequest(AccountType accountType, Guid userID)
        {

            AccountType = accountType;
            UserID = userID;
        }
    }
}
