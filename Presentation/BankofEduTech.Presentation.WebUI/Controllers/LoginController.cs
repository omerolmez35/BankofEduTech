using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.Presentation.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
