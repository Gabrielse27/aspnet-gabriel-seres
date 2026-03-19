using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult My()
        {
            return View();
        }
    }
}
