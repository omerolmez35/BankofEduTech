using BankofEduTech.Core.Application.Repositories.CustomerCredits;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerCredits
{
    public class CustomerCreditsWriteRepository : WriteRepository<BankofEduTech.Core.Domain.Entities.CustomerCredits>, ICustomerCreditWriteRepository
    {
        public CustomerCreditsWriteRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
