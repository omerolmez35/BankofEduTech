using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId
{
    public class GetApplicationByUserIdQueryRequest : IRequest<List<GetApplicationByUserIdQueryResponse>>
    {
        public Guid UserID { get; set; }

        public GetApplicationByUserIdQueryRequest(Guid userID)
        {
            UserID = userID;
        }
    }
}
