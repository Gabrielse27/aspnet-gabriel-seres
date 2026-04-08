using CoreFitness.Domain.Identity;
using CoreFitness.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace CoreFitness.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Memberships()
        {
            return View();
        }

        private readonly SignInManager<User> _signInManager;

        // 2. Skapa en konstruktor som hämtar in SignInManager
        public HomeController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }


        public IActionResult CustomerService()
            {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Detta tar bort inloggningen
            return RedirectToAction("Index", "Home"); // Skickar tillbaka användaren till startsidan
        }

        public IActionResult Padel()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}
