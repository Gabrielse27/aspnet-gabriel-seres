
using CoreFitness.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;



namespace CoreFitness.Web.Areas.Identity.Pages.Account
{
    public class SetPasswordModel : PageModel
    {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // 2. Skapa konstruktorn (Denna hämtar in verktygen från systemet)
        public SetPasswordModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }




        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [StringLength(100, MinimumLength = 8)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required]
            public bool AcceptTerms { get; set; }
        }

        public void OnGet(string email = "")
        {
            Input.Email = email; // Förifyll e-post om den skickas från förra sidan
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                // 2. Om användaren INTE finns i AspNetUsers, skapa den nu!
                user = new User { UserName = Input.Email, Email = Input.Email };
                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                {
                    // Om något gick fel vid skapandet, visa felen
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
                var addPasswordResult = await _userManager.AddPasswordAsync
                    (user, Input.Password);

                if (addPasswordResult.Succeeded)
                {
                    // 1. Logga in användaren automatiskt så de slipper göra det igen
                    await _signInManager.SignInAsync(user, isPersistent: false);


                    // 2. ÄNDRA DENNA RAD för att gå till din profil istället för Home
                    return RedirectToAction("Index", "Account", new { area = "" });
                }

                // Lägg till felmeddelanden om lösenordet är för svagt
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Index", "Account", new { area = "" });
           
        }
       
    }
    
}