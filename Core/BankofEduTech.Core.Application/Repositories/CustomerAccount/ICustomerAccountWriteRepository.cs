using BankofEduTech.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Repositories.CustomerAccount
{
    public interface ICustomerAccountWriteRepository : IWriteRepository<Domain.Entities.CustomerAccount>
    {
    }
}
