using Microsoft.AspNetCore.Mvc;

namespace IndexWMSProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Wms");
        }
    }
}
