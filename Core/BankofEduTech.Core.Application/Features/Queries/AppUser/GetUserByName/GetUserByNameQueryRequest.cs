using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName
{
    public class GetUserByNameQueryRequest : IRequest<GetUserByNameQueryResponse>
    {
        public Guid UserID { get; set; }

        public GetUserByNameQueryRequest(Guid userID)
        {
            UserID = userID;
        }
    }
}
