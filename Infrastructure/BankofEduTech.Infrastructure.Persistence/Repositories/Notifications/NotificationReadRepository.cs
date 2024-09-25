using BankofEduTech.Core.Application.Repositories.Notifications;
using BankofEduTech.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Persistence.Repositories.Notifications
{
    public class NotificationReadRepository : ReadRepository<BankofEduTech.Core.Domain.Entities.Notifications>, INotificationReadRepository
    {
        public NotificationReadRepository(BankofEduTechContext context) : base(context)
        {
        }
    }
}
