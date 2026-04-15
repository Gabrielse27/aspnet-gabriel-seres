using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreFitness.Infrastructure.Persistence.Contexts;
using CoreFitness.Domain.Entities;


namespace CoreFitness.Web.Controllers
{

    [Authorize] // Endast Inloggade kan se admin-sidan
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        public AdminController(DataContext context)
        {
            _context = context;
        }
        [Authorize] // Endast användare med rollen "Admin" kan komma åt denna action
        public IActionResult CreateSession()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(GymSession session)
        {
                       if (ModelState.IsValid)
            {
                _context.GymSessions.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Training");
            }
            return View(session);
        
        }

    }


}