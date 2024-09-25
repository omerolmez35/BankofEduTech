using BankofEduTech.Core.Application.Repositories.AppUser;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BankofEduTech.Infrastructure.Persistence.Repositories.AppUser
{
    public class AppUserWriteRepository : WriteRepository<BankofEduTech.Core.Domain.Entities.AppUser>, IAppUserWriteRepository
    {
        public AppUserWriteRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
