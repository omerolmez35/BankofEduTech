using BankofEduTech.Core.Application.Repositories.CustomerAccount;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerAccount
{
    public class CustomerAccountReadRepository : ReadRepository<BankofEduTech.Core.Domain.Entities.CustomerAccount>, ICustomerAccountReadRepository
    {
        public CustomerAccountReadRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
