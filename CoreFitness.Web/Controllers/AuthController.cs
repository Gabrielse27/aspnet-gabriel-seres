using CoreFitness.External.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Web.Controllers;

public class AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser>userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> SignIn(string? returnUrl = null)
    {
        var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();

        var vm = new SignInViewModel
        {
            ReturnUrl = returnUrl,
            ExternalProviders = [.. schemes.Select(x => x.Name)]
        };

        return View(vm);
    }
}
