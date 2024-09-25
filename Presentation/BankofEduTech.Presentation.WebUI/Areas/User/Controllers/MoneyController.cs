using BankofEduTech.Core.Application.Features.Commands.AppUser.Update;
using BankofEduTech.Core.Application.Features.Commands.UserMoney.SendMoney;
using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAccountInfo;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Presentation.WebUI.Services.SignalRService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Net.Http;
using System.Text;

namespace BankofEduTech.Presentation.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class MoneyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClaimService _claimService;
        private readonly ISignalRService _signalRService;

        public MoneyController(IHttpClientFactory httpClientFactory, ClaimService claimService, ISignalRService signalRService)
        {
            _httpClientFactory = httpClientFactory;
            _claimService = claimService;
            _signalRService = signalRService;
        }

        public async Task<IActionResult> Index()

        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(new GetAllAccountForByUserIdQueryRequest { UserID = Guid.Parse(_claimService.Surname) });
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync($"https://localhost:7244/api/UserMoney/GetAllAccountForByUserId", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetAllAccountForByUserIdQueryResponse>>(responseContent);

                ViewBag.Accounts = result;
                return View();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SendMoneyCommandRequest request)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserMoney/SendMoney", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SendMoneyCommandResponse>(responseContent);
                await _signalRService.SendUserNotification(result.SenderID.ToString(), $"Hesabınızdan {request.Amount} TL para çıkışı olmuştur.",NotificationType.GidenPara);
                await _signalRService.SendUserNotification(result.ReceiverID.ToString(), $"Hesabınıza {request.Amount} TL para girişi olmuştur.", NotificationType.GelenPara);
                await _signalRService.GetCurrentNotificationAsync(result.SenderID.ToString());
                await _signalRService.GetCurrentNotificationAsync(result.ReceiverID.ToString());

                return Ok(result);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetAccountInfo(GetAccountInfoQueryRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserMoney/GetAccountInfo", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetAccountInfoQueryResponse>(responseContent);


                return Ok(result);
            }
            else return Ok(new GetAccountInfoQueryResponse() { IsSuccess = false, Message = "İşlem Başarısız." });

        }

        [HttpPost]
        public IActionResult GetAllAccountForByUserId(GetAllAccountForByUserIdQueryRequest request)
        {

            return Ok();
        }
    }
}
