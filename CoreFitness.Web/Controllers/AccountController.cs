using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Members.Inputs;
using CoreFitness.Application.Services;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Identity;
using CoreFitness.Domain.Repositoryes.Members;
using CoreFitness.Infrastructure.Persistence.Contexts;
using CoreFitness.Infrastructure.Repositories.Members;
using CoreFitness.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
       
        // 2. Ta emot den i constructorn
        public AccountController (UserManager<User> userManager, SignInManager<User> signInManager, DataContext context,
            IGymService gymService, 
            IMemberRepository memberRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _gymService = gymService;
            _memberRepository = memberRepository;   
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


        [HttpPost]
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

        [HttpPost]
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


        [HttpPost]
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


