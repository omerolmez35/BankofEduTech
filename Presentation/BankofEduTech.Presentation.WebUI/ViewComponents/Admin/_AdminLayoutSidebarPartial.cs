using Microsoft.AspNetCore.Mvc;

namespace BankofEduTech.Presentation.WebUI.ViewComponents.Admin
{
    public class _AdminLayoutSidebarPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
