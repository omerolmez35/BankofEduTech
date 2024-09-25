using BankofEduTech.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification
{
    public class GetActiveLastNotificationQueryResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }
    }
}
