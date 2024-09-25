using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.GetAllUser
{
    public class GetAllUsersQueryRequest : IRequest<List<GetAllUsersQueryResponse>>
    {
    }
}
