using Microsoft.AspNetCore.Mvc;

namespace WebApplicationIntcomex.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Client()
        {
            return RedirectToAction("Index", "Client");
        }
    }
}
