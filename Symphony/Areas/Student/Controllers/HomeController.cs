using Microsoft.AspNetCore.Mvc;

namespace Symphony.Areas.Student.Controllers
{
    [Area("student")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
