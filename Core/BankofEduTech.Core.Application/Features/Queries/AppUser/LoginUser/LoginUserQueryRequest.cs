using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser
{
    public class LoginUserQueryRequest : IRequest<LoginUserQueryResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
