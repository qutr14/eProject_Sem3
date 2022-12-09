using Microsoft.AspNetCore.Mvc;

namespace Symphony.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
