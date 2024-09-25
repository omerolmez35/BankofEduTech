using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.Presentation.WebUI.ViewComponents.Customer
{
    public class _CustomerLayoutHeaderPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
