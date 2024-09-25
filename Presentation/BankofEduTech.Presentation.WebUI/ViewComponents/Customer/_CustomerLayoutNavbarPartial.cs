using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BankofEduTech.Presentation.WebUI.ViewComponents.Customer
{
    public class _CustomerLayoutNavbarPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClaimService _claimService;

        public _CustomerLayoutNavbarPartial(IHttpClientFactory httpClientFactory, ClaimService claimService)
        {
            _httpClientFactory = httpClientFactory;
            _claimService = claimService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/Notification/GetActiveLastNotification?userID={Guid.Parse(_claimService.Surname)}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetActiveLastNotificationQueryResponse>>(responseContent);
                ViewData["Notifications"] = result;
                return View();
            }
            return View();

        }
    }
}
