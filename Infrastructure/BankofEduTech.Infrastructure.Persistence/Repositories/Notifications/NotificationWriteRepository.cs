using BankofEduTech.Core.Application.Repositories.Notifications;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.Notifications
{
    public class NotificationWriteRepository : WriteRepository<BankofEduTech.Core.Domain.Entities.Notifications>, INotificationWriteRepository
    {
        public NotificationWriteRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
