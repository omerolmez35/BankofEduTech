using BankofEduTech.Core.Application.Repositories.CustomerCreditsInstallements;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerCreditsInstallements
{
    public class CustomerCreditsInstallementsWriteRepository : WriteRepository<BankofEduTech.Core.Domain.Entities.CustomerCreditInstallements>, ICustomerCreditInstallementsWriteRepository
    {
        public CustomerCreditsInstallementsWriteRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
