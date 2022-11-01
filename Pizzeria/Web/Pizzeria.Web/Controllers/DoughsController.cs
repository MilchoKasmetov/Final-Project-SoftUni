using Microsoft.AspNetCore.Mvc;

namespace Pizzeria.Web.Controllers
{
    public class DoughsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
