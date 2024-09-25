using BankofEduTech.Core.Application.Repositories.CustomerCredits;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerCredits
{
    public class CustomerCreditsReadRepository : ReadRepository<BankofEduTech.Core.Domain.Entities.CustomerCredits>, ICustomerCreditReadRepository
    {
        public CustomerCreditsReadRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
