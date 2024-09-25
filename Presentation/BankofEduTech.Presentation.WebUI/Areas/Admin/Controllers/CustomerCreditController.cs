using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCreditApplication;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCustomerCreditApplication;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Core.Domain.Entities;
using BankofEduTech.Presentation.WebUI.Services.SignalRService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BankofEduTech.Presentation.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]

    public class CustomerCreditController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISignalRService _signalRService;

        public CustomerCreditController(IHttpClientFactory httpClientFactory, ISignalRService signalRService)
        {
            _httpClientFactory = httpClientFactory;
            _signalRService = signalRService;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/AdminCustomer/GetCustomerCreditApplication");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetCustomerCreditApplicationQueryResponse>>(responseContent);
                return View(result);
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCredit([FromBody] CreateUserCreditCommandRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserCredit/CreateCredit", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateUserCreditCommandResponse>(responseContent);
                if(result.CreditStatus == CreditStatus.Onay) await _signalRService.SendUserNotification(result.UserID.ToString(), "Kredi başvurunuz onaylandı ve hesabınıza aktarıldı. İyi günlerde kullanın.", NotificationType.KrediSonuc);
                else await _signalRService.SendUserNotification(result.UserID.ToString(), "Üzgünüz... Kredi başvurunuz maalesef şu an için olumlu sonuçlanmadı. Daha sonra tekrar deneyebilirsiniz.", NotificationType.KrediSonuc);

                await _signalRService.GetCurrentNotificationAsync(result.UserID.ToString());



                return Ok(result);
            }

            return Ok();

        }
    }
}
