using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Identity;
using CoreFitness.Infrastructure.Persistence.Contexts; // Dubbelkolla att detta stämmer med din DataContext-mapp
using CoreFitness.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CoreFitness.Web.Services;


namespace CoreFitness.Web.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        // Vi tar bort DataContext helt och hållet från kontrollern!
        private readonly ITrainingService _trainingService;
        private readonly UserManager<User> _userManager;

        public TrainingController(ITrainingService trainingService, UserManager<User> userManager)
        {
            _trainingService = trainingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string category)
        {
            // Kontrollern vet inte hur data hämtas, den bara frågar servicen
            var sessions = await _trainingService.GetAllSessionsAsync(category);
            return View(sessions);
        }

        [HttpPost]
        public async Task<IActionResult> Book(int sessionId, string? category = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                await _trainingService.BookSessionAsync(sessionId, user.Id);

            return RedirectToAction(nameof(Index), new { category });
        }

        [HttpPost]
        public async Task<IActionResult> Unbook(int sessionId, string? category = null)
        {
            await _trainingService.UnBookSessionAsync(sessionId);
            return RedirectToAction(nameof(Index), new { category });
        }
    }
}