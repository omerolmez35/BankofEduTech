using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Features.Commands.AppUser.Update;
using BankofEduTech.Core.Application.Features.Commands.UserAccount.Create;
using BankofEduTech.Core.Application.Features.Queries.Customer.GetAllCustomer;
using BankofEduTech.Core.Application.Features.Queries.CustomerCredit.GetPaidPaymentsForCustomer;
using BankofEduTech.Core.Application.Features.Queries.UserAccount.GetAllUserAccountActiveUser;
using BankofEduTech.Core.Application.StaticServices.BankofEduTech.Core.Application.StaticServices;
using BankofEduTech.Core.Application.ViewModels;
using BankofEduTech.Presentation.WebUI.SignalRHubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BankofEduTech.Presentation.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHubContext<NotificationHub> _hubContext;

        public CustomerController(IHttpClientFactory httpClientFactory, IHubContext<NotificationHub> hubContext)
        {
            _httpClientFactory = httpClientFactory;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = ClaimService.CurrentUser.Surname;
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/AdminCustomer/GetAllCustomer");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetAllCustomerQueryResponse>>(responseContent);

                return View(result);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CustomerDetail(Guid customerID)
        {
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/AdminCustomer/CustomerDetail?customerID={customerID}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerDetailViewModel>(responseContent);

                return View(result);
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomerAccount([FromBody] CreateUserAccountCommandRequest request)
        {
            
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

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateAppUserCommandRequest request)
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

        [HttpGet]
        public async Task<IActionResult> GetCustomerCreditPaidPayments(Guid userID)
        {
   
            var client = _httpClientFactory.CreateClient();
            var resMsg = await client.GetAsync($"https://localhost:7244/api/AdminCustomer/GetCustomerCreditPaidPayments?userID={userID}");

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<GetPaidPaymentsForCustomerQueryResponse>>(responseContent);

                return Ok(result);
            }

            return Ok();
        }
    }
}
