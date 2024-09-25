using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.Notification.GetActiveLastNotification;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace BankofEduTech.Presentation.WebUI.Controllers
{
    public class UserLayoutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClaimService _claimService;

        public UserLayoutController(IHttpClientFactory httpClientFactory, ClaimService claimService)
        {
            _httpClientFactory = httpClientFactory;
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
    }
}
