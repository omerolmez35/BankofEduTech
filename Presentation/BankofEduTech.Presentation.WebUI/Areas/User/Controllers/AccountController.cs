using AspNetCoreHero.ToastNotification.Abstractions;
using BankofEduTech.Core.Application.Features.Commands.AppUser.Update;
using BankofEduTech.Core.Application.Features.Commands.UserAccount.Create;
using BankofEduTech.Core.Application.Features.Queries.AppUser.GetUserByName;
using BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace BankofEduTech.Presentation.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly INotyfService _notyf;
        private readonly ClaimService _claimService;

        public AccountController(IHttpClientFactory httpClientFactory, INotyfService notyf, ClaimService claimService)
        {
            _httpClientFactory = httpClientFactory;
            _notyf = notyf;
            _claimService = claimService;
        }

        public async Task<IActionResult> Index()
        {
            Guid userID = Guid.Parse(_claimService.Surname);
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/Account/GetActiveUserByName?userID={userID}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetUserByNameQueryResponse>(responseContent);


                if (!result.IsSuccess)
                {
                    _notyf.Error(result.Message);

                    return View();
                }
                else
                {
                    return View(result);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] UpdateAppUserCommandRequest request)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/Account/UpdateUser", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UpdateAppUserCommandResponse>(responseContent);


                return Ok(result);
            }

            return Ok();

        }

        public IActionResult CreateAccount()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateUserAccountCommandRequest request)
        {
            request.UserID = Guid.Parse(_claimService.Surname);
            
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserAccount/CreateUserAccount", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateUserAccountCommandResponse>(responseContent);


                return Ok(result);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            var user = ClaimService.CurrentUser.Surname;
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserAccount/GetAllAccount?userID={user}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetAllAccountQueryResponse>>(responseContent);


                if (!result[0].IsSuccess)
                {
                    return View();
                }
                else
                {
                    return View(result);
                }
            }

            return View();
        }





        [HttpGet]
        public async Task<IActionResult> GetAllAccountActivities(int accountID)
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserAccount/GetAllAccountActivities?accountID={accountID}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetAccountActivitiesQueryResponse>>(responseContent);


                if (!result[0].IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok(result);
                }
            }

            return View();
        }
    }
}
