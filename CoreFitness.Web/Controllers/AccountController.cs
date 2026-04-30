using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Services;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Identity;
using CoreFitness.Domain.Repositoryes.Members;
using CoreFitness.Infrastructure.Persistence.Contexts;
using CoreFitness.Infrastructure.Repositories.Members;
using CoreFitness.Web.Models;
using CoreFitness.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoreFitness.Web.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {

        // 1. Skapa en privat variabel
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;
        private readonly IGymService _gymService;
        private readonly IMemberRepository _memberRepository;
        private readonly IBookingService _bookingService;

       
        // 2. Ta emot den i constructorn
        public AccountController (UserManager<User> userManager, SignInManager<User> signInManager, DataContext context,
            IGymService gymService, 
            IMemberRepository memberRepository, IBookingService bookingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _gymService = gymService;
            _memberRepository = memberRepository;
            _bookingService = bookingService;
        }




        // Google Redirecting
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null!)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null!, string remoteError = null!)
        {
            if (remoteError != null) return RedirectToAction("Login", "Account");

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction("Login", "Account");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }

            else
            {
                // 1. Hämta e-postadressen från Googles information
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // 2. Skapa användarobjektet automatiskt i bakgrunden
                    var user = new User { UserName = email, Email = email };

                    // 3. Försök skapa användaren i din databas
                    var creationResult = await _userManager.CreateAsync(user);

                    if (creationResult.Succeeded)
                    {
                        // 4. Koppla ihop Google-kontot med din nya användare
                        await _userManager.AddLoginAsync(user, info);

                        // 5. Logga in användaren direkt!
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        // 6. Skicka dem till startsidan (eller där de kom ifrån)
                        return LocalRedirect(returnUrl ?? "/");
                    }
                }

                // Om något gick fel (t.ex. om e-posten redan finns), skicka till vanlig login
                return RedirectToAction("Login", "Account");
            }

        }





        // 3. HÄR ÄR DIN NYA METOD
        public async Task<IActionResult> MyBookings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge(); // Skickar till inloggning om ej inloggad

            // Anropar tjänsten
            var bookings = await _bookingService.GetUserBookingsAsync(user.Id);

            return View(bookings);
        }












        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Detta raderar användarens inloggnings-cookie
            await _signInManager.SignOutAsync();

            // Skicka tillbaka användaren till startsidan
            return RedirectToAction("Index", "Home");
        }







        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            // Hämta informationen som Google skickade med (t.ex. e-post)
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction("Login");

            // Skapa en modell som förbereder vyn med användarens e-post
            var model = new RegisterViewModel
            {
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // Koppla ihop det nya kontot med Google-inloggningen
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl ?? "/");
                }
            }
            return View(model);
        }








        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var viewModel = new AccountDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                // HÄR ÄR FIXEN:
                // Om user.ProfileImage har ett värde i databasen, använd det.
                ProfileImageUrl = !string.IsNullOrEmpty(user.ProfileImage)
                    ? "/images/" + user.ProfileImage
                    : "/images/photo-profile-myaccount.svg"
            };

            return View(viewModel);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AccountDetailsViewModel model)
        {

            if (!ModelState.IsValid)
            {
                // Om valideringen misslyckas, skicka tillbaka användaren till vyn direkt
                // utan att spara något i databasen. Felmeddelandena visas nu i din HTML.
                return View(model);
            }


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
                user.ProfileImage = fileName;

                model.ProfileImageUrl = "/images/" + fileName;
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


        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(CreateClientFormModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Skapa ett objekt som ska sparas i databasen
                // (Antingen har du en tabell som heter ContactRequest eller Messages)
                var contactRequest = new ContactRequestEntity
                {
                    FirstName = model.FirstName!,
                    LastName = model.LastName!,
                    Email = model.Email!,
                    PhoneNumber = model.PhoneNumber,
                    Message = model.Message,
                    CreatedDate = DateTime.Now // Bra att ha för att veta när det skickades
                };

                // 2. Lägg till i databas-contextet
                _context.Messages.Add(contactRequest);

                // 3. Spara ändringarna till databasen på riktigt
                await _context.SaveChangesAsync();

                // Skicka användaren vidare till en tack-sida eller startsidan
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/CustomerService.cshtml", model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinMembership(int membershipId)
        {
            // Hämta inloggad användare
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Skapa "paketet" till din tjänst
            var input = new JoinMembershipInput
            {
                UserId = user.Id,
                MembershipId = membershipId
            };

            // Anropa din snygga VG-logik!
            var result = await _gymService.JoinMembershipAsync(input);

            if (result.IsSuccess)
            {
                TempData["StatusMessage"] = "Välkommen som medlem!";
                return RedirectToAction("Index"); // Skicka till "Mina sidor"
            }

            TempData["ErrorMessage"] = result.Error;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Membership()
        {
            var userId = _userManager.GetUserId(User);
            // Hämta medlemmen inkl. MembershipId från databasen
            var member = await _memberRepository.GetMemberByUserIdAsync(userId);

            if (member == null)
            {
                return View("NoMembership"); // Om användaren inte har gått med än
            }

            return View(member);
        }


        
    }



}


