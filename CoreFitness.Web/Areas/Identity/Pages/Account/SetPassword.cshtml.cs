using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Web.Areas.Identity.Pages.Account
{
    public class SetPasswordModel : PageModel
    {
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

            // HÄR: Lägg till logiken för att spara lösenordet i databasen

            return RedirectToPage("RegisterConfirmation");
        }
    }
}