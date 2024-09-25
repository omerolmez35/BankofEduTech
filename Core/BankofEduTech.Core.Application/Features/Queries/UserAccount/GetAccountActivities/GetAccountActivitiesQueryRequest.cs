using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities
{
    public class GetAccountActivitiesQueryRequest : IRequest<List<GetAccountActivitiesQueryResponse>>
    {
        public int AccountID { get; set; }

        public GetAccountActivitiesQueryRequest(int accountID)
        {
            AccountID = accountID;
        }
    }
}
