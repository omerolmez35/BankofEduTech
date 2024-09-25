using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities;
using BankofEduTech.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Presentation.WebUI.Services.SignalRService

{
    public interface ISignalRService
    {
        Task SendUserNotification(string userId, string message, NotificationType type);
        Task<List<GetActiveLastNotificationQueryResponse>> GetCurrentNotificationAsync(string userId);
    }
}
