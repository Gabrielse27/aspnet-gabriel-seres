using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
