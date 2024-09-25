using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Features.Commands.Notification.Create;
using BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney;
using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Presentation.WebUI.SignalRHubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Presentation.WebUI.Services.SignalRService
{
    public class SignalRService : ISignalRService
    {
        //private readonly ClaimService _claimService;
        //private readonly IHttpClientFactory _httpClientFactory;



        //public SignalRService(ClaimService claimService, IHttpClientFactory httpClientFactory)
        //{
        //    _claimService = claimService;
        //    _httpClientFactory = httpClientFactory;
        //}

        //public async Task<List<GetActiveLastNotificationQueryResponse>> SendRealTimeNotificationsAsync()
        //{
        //    Guid userID = Guid.Parse(_claimService.Surname);
        //    var client = _httpClientFactory.CreateClient();
        //    var resMsg = await client.GetAsync($"https://localhost:7244/api/Notification/GetActiveLastNotification?userID={userID}");

        //    if (resMsg.IsSuccessStatusCode)
        //    {
        //        var responseContent = await resMsg.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<List<GetActiveLastNotificationQueryResponse>>(responseContent);

        //        return result;

        //    }

        //    return new List<GetActiveLastNotificationQueryResponse>();
        //   // var notifications = await _mediator.Send(new GetActiveLastNotificationQueryRequest(Guid.Parse(_claimService.Surname)));

        //}
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public SignalRService(IHubContext<NotificationHub> hubContext, IHttpClientFactory httpClientFactory)
        {
            _hubContext = hubContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GetActiveLastNotificationQueryResponse>> GetCurrentNotificationAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient();

            var resMsg = await client.GetAsync($"https://localhost:7244/api/Notification/GetActiveLastNotification?userID={userId}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetActiveLastNotificationQueryResponse>>(responseContent);


               await _hubContext.Clients.User(userId).SendAsync("ReceiveCurrentNotification", result);
                return result;

            }
            return new List<GetActiveLastNotificationQueryResponse>();

        }

        public async Task SendUserNotification(string userId, string message, NotificationType type)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(new CreateNotificiationCommandRequest { CreatedDate = DateTime.Now, Message = message, NotificationType = type, UserID = Guid.Parse(userId) });
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/Notification/CreateNotification", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SendMoneyCommandResponse>(responseContent);


                if (result.IsSuccess) await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);

            }




        }
    }
}
