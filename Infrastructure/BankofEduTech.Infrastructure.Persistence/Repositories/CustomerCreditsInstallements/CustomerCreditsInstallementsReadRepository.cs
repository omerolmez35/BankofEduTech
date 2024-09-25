using BankofEduTech.Core.Application.Repositories.CustomerCreditsInstallements;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerCreditsInstallements
{
    public class CustomerCreditsInstallementsReadRepository : ReadRepository<BankofEduTech.Core.Domain.Entities.CustomerCreditInstallements>, ICustomerCreditInstallementsReadRepository
    {
        public CustomerCreditsInstallementsReadRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
