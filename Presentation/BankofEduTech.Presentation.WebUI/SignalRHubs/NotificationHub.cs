using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Presentation.WebUI.Services.SignalRService;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BankofEduTech.Presentation.WebUI.SignalRHubs
{
    public class NotificationHub : Hub
    {
        private readonly ISignalRService _signalRService;
        private readonly ClaimService _claimService;

        public NotificationHub(ISignalRService signalRService, ClaimService claimService)
        {
            _signalRService = signalRService;
            _claimService = claimService;
        }
        public async Task CurrentNotification()
        {
            var notifications = await _signalRService.GetCurrentNotificationAsync(_claimService.Surname);
        }

        public override async Task OnConnectedAsync()
        {
            string userId = _claimService.Surname;

            await base.OnConnectedAsync();
        }
    }
}
