using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserInfo;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using Uc = BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices.ClaimService;
namespace BankofEduTech.Presentation.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserAccount/GetUserInfo");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetUserInfoQueryResponse>(responseContent);


                if (!result.IsSuccess)
                {
                    return View(result);
                }
                else
                {
                    return View(result);
                }
            }


            var ad = Uc.CurrentUser.Name;
            ViewBag.AdName = ad;
            return View();
        }
    }
}
