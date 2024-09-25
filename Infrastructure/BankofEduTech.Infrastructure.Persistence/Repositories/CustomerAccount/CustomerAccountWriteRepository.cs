using BankofEduTech.Core.Application.Repositories.CustomerAccount;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerAccount
{
    public class CustomerAccountWriteRepository : WriteRepository<BankofEduTech.Core.Domain.Entities.CustomerAccount>, ICustomerAccountWriteRepository
    {
        public CustomerAccountWriteRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
