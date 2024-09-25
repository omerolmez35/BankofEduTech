using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserInfo
{
    public class GetUserInfoQueryRequest : IRequest<GetUserInfoQueryResponse>
    {
        public Guid UserID { get; set; }
    }
}
