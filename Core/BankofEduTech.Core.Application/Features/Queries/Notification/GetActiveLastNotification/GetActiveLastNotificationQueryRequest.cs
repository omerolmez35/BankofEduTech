using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification
{
    public class GetActiveLastNotificationQueryRequest : IRequest<List<GetActiveLastNotificationQueryResponse>>
    {
        public Guid UserID { get; set; }

        public GetActiveLastNotificationQueryRequest(Guid userID)
        {
            UserID = userID;
        }
    }
}
