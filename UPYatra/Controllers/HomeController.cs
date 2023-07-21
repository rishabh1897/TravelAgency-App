using Microsoft.AspNetCore.Mvc;

namespace UPYatra.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
