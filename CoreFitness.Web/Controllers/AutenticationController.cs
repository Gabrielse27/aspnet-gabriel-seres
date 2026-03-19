using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers
{
    public class AutenticationController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult RegisterEmail()
        {
            return View();
        }


        public IActionResult RegisterPassword()
        {
            return View();
        }

        public IActionResult RegisterProfile()
        {
            return View();
        }

    }
}
