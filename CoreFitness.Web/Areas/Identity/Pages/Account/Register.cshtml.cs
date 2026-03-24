using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        // Denna behövs för @Model.ReturnUrl
        public string ReturnUrl { get; set; }

        // Denna behövs för asp-for="Input.Email"
        [BindProperty]
        public InputModel Input { get; set; }

       

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [Display(Name = "Accept user terms and conditions")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

                returnUrl ??= Url.Content("~/");

            // Den här raden skickar iväg användaren till nästa sida direkt
            return RedirectToPage("SetPassword", new { email = Input.Email });
            
           
        }
    }
}