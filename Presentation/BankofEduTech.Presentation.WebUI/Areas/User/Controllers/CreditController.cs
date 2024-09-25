using BankofEduTech.Core.Application.Features.Commands.AppUser.Update;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCredit;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.CreateUserCreditApplication;
using BankofEduTech.Core.Application.Features.Commands.UserCredit.UserCreditPay;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.CustomerCreditCalculation;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetApplicationByUserId;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetCreditPayments;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPayCredit;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAccountActivities;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Application.Features.Queries.UserMoney.GetAllAccountForByUserId;
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
    public class CreditController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClaimService _claimService;

        public CreditController(IHttpClientFactory httpClientFactory, ClaimService claimService)
        {
            _httpClientFactory = httpClientFactory;
            _claimService = claimService;
        }

        public async Task<IActionResult> Index()
        {
            var user = ClaimService.CurrentUser.Surname;
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserAccount/GetAllCreditAccounts?userID={user}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetAllAccountQueryResponse>>(responseContent);


                if (!result[0].IsSuccess)
                {
                    return View(result);
                }
                else
                {
                    var clientAccount = _httpClientFactory.CreateClient();
                    var resMsgAccount = await client.GetAsync($"https://localhost:7244/api/UserAccount/GetAllAccount?userID={user}");
                    if (resMsg.IsSuccessStatusCode)
                    {
                        var responseContentAccount = await resMsgAccount.Content.ReadAsStringAsync();
                        var resultAccount = JsonConvert.DeserializeObject<List<GetAllAccountQueryResponse>>(responseContentAccount);

                        ViewBag.Accounts = resultAccount;


                    }
                    return View(result);
                }
            }





            return View();

        }

        [HttpGet]
        public async Task<IActionResult> CreditApplication()
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(new GetAllAccountForByUserIdQueryRequest { UserID = Guid.Parse(_claimService.Surname)});
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserMoney/GetAllAccountForByUserId", stringContent);

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
        public async Task<IActionResult> CalculateCredit([FromBody] CustomerCreditCalculationQueryRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserCredit/CreditCalculate", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CustomerCreditCalculationQueryResponse>>(responseContent);

                return Ok(result);
            }

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCreditsAccount()
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserCredit/GetAllCreditsAccount");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetAllAccountQueryResponse>>(responseContent);


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

        [HttpPost]
        public async Task<IActionResult> CreateCreditApplication([FromBody] CreateUserCreditApplicationCommandRequest request)
        {
            request.UserID = Guid.Parse(_claimService.Surname);
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserCredit/CreateCreditApplication", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateUserCreditApplicationCommandResponse>(responseContent);


                return Ok(result);
            }

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> GetCreditPaymentTable(int accountID)
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserCredit/GetCreditPaymentTable?accountID={accountID}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetCreditPaymentsQueryResponse>>(responseContent);


                if (result.Count > 0)
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

        [HttpGet]
        public async Task<IActionResult> GetPayCredit(int installementID)
        {

            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserCredit/GetPayCredit?installementID={installementID}&userID={_claimService.Surname}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetPayCreditQueryResponse>(responseContent);


                if (!result.IsSuccess)
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

        [HttpPost]
        public async Task<IActionResult> CreditPay(int senderID, int installementsID, decimal delayInterest)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(new UserCreditPayCommandRequest { DelayInterest = delayInterest, SenderID = senderID, InstallementsID = installementsID });
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/UserCredit/CreditPay", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UserCreditPayCommandResponse>(responseContent);

                return Ok(result);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetApplication()
        {

            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/UserCredit/GetCreditApplications?userID={_claimService.Surname}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetApplicationByUserIdQueryResponse>>(responseContent);


                return View(result);
            }

            return View();
        }
    }
}