using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CoreFitness.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreFitness.Web.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {

        // 1. Skapa en privat variabel
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        // 2. Ta emot den i constructorn
        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // Vi skapar modellen med infon vi ser i din databasbild
            var model = new AccountDetailsViewModel
            {
                FirstName = user.FirstName, // Här hämtas från SQL
                LastName = user.LastName,   
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfilePicture != null ? "/images/" + user.ProfilePicture : "/images/default.png"

             
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(AccountDetailsViewModel model)
        {
            


            // 1. Hämta den inloggade användaren från databasen
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // 2. Hantera bildfilen (om användaren har valt en ny bild)
            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                // Skapa sökvägen till wwwroot/images
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                // Skapa ett unikt filnamn för att undvika krockar
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(folder, fileName);

                // Spara filen fysiskt på servern
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                // Uppdatera användarobjektet med det nya filnamnet
                user.ProfilePicture = fileName;
            }

            // 3. Uppdatera textfälten från formuläret
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            // 4. Spara ALLA ändringar till SQL-databasen
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["StatusMessage"] = "Din profil har uppdaterats!";
                return RedirectToAction(nameof(Index)); // Laddar om sidan så att ny data visas
            }

            // Om något gick fel, visa vyn igen med felmeddelanden
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // 1. Ta bort användaren från SQL-databasen
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // 2. Logga ut användaren så att cookien försvinner
                await _signInManager.SignOutAsync();

                // 3. Skicka tillbaka till startsidan
                return RedirectToAction("Index", "Home");
            }

            // Om något gick fel
            return RedirectToAction(nameof(Index));
        }


    }

}


