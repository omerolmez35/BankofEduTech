using BankofEduTech.Core.Application.Repositories.CustomerAccountActivities;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.CustomerAccountActivities
{
    public class CustomerAccountActivitiesWriteRepository : WriteRepository<BankofEduTech.Core.Domain.Entities.CustomerAccountActivities>, ICustomerAccountActivitiesWriteRepository
    {
        public CustomerAccountActivitiesWriteRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
