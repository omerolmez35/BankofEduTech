using BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Abstractions
{
    public interface ITokenService
    {
        string CreateToken(LoginUserQueryResponse response);
    }
}
