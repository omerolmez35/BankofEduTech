using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.Presentation.WebUI.ViewComponents.Customer
{
    public class _CustomerLayoutScriptPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
