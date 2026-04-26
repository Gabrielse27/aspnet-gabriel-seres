using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Infrastructure.Persistence.Contexts; // Dubbelkolla att detta stämmer med din DataContext-mapp
using CoreFitness.Domain.Entities;
using CoreFitness.Web.Models;
using Microsoft.IdentityModel.Tokens;
using CoreFitness.Domain.Identity;



namespace CoreFitness.Web.Controllers
{
    [Authorize]  // Endast Inloggade kan se boka/pass
    public class TrainingController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;


        public TrainingController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // Visa alla pass
        public async Task <IActionResult> Index(string category)
        {
            var user = await _userManager.GetUserAsync(User);

                // Hämtar passen från databasen
                var query = _context.GymSessions.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category == category);
            }

            var sessions = await query.ToListAsync();

            return View(sessions);
        }


        // Logik för att boka

        [HttpPost]
        public async Task<IActionResult> Book(int sessionId)
        {
            var user = await _userManager.GetUserAsync(User);
            var session = await _context.GymSessions.FindAsync(sessionId);

            if (session != null && user != null) 
            {
                session.BookedByUserId = user.Id;
                await _context.SaveChangesAsync();

               
            }
            return RedirectToAction(nameof(Index));

        }
        //  Logik för att Avboka
        [HttpPost]
        public async Task<IActionResult> Unbook(int sessionId)
        {
            var session = await _context.GymSessions.FindAsync(sessionId);

            if (session != null)
            {
                session.BookedByUserId = null; // Tar bort bokningen
                await _context.SaveChangesAsync();

                
            }
            return RedirectToAction(nameof(Index));
        }
    }    
}   

