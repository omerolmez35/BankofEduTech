using AspNetCoreHero.ToastNotification.Abstractions;
using BankofEduTech.Core.Application.Features.Commands.AppUser.Create;
using BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser;
using BankofEduTech.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text;

namespace BankofEduTech.Presentation.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly INotyfService _notyf;
        public LoginController(IHttpClientFactory httpClientFactory, INotyfService notyf)
        {
            _httpClientFactory = httpClientFactory;
            _notyf = notyf;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginUserQueryRequest request)
        {
            var client = _httpClientFactory.CreateClient();



            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/Login/SignIn", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginUserQueryResponse>(responseContent);


                if (!result.IsSuccess)
                {
                    _notyf.Error(result.Message);

                    return View();
                }
                else
                {
                    _notyf.Success(result.Message);
                    HttpContext.Session.SetString("JWToken", result.Token);
                    if (result.Roles[0] == "Admin") return RedirectToAction("Index", "Home", new { area = "Admin" });
                    else if (result.Roles[0] == "Customer") return RedirectToAction("Index", "Home", new { area = "User" });
                }
            }

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateAppUserCommandRequest request)
        {

            request.City = "İzmir";
            request.District = "Buca";
            request.ImageUrl = "deneme";


            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/Login/CreateUser", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateAppUserCommandResponse>(responseContent);


                if (!result.IsSuccess)
                {
                    foreach (var item in result.Messages)
                    {
                        ModelState.AddModelError("", item);
                    }
                    return View();
                }
                else
                {
                    _notyf.Success("Kayıt işleminiz başarılıdır. Mailinize gelen linke tıklayarak doğrulama yaptıktan sonra giriş yapabilirsiniz.");
                    return RedirectToAction("Index", "Login");
                }
            }

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> ActivationMail(EmailActivationModel model)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var resMsg = await client.PostAsync("https://localhost:7244/api/Login/ActivationMail", stringContent);

            if (resMsg.IsSuccessStatusCode)
            {
                var responseContent = await resMsg.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResultModel>(responseContent);


                if (!result.IsSuccess)
                {
                    _notyf.Error("Doğrulama sırasında hata oluştu");
                    return View();
                }
                else
                {
                    _notyf.Success("Doğrulama işleminiz başarılıdır. Giriş yapabilirsiniz.");
                    return RedirectToAction("Index", "Login");
                }


            }
            return View();
        }

        public IActionResult Logout()
        {
           
            HttpContext.Session.Remove("JWToken"); 
            return RedirectToAction("Index");
        }
    }
}
