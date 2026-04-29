using CoreFitness.Infrastructure.Persistence.Contexts;
using CoreFitness.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace CoreFitness.Web.Repositories
{
    // Vi lägger till ": IGymSessionRepository" för att implementera kontraktet
    public class GymSessionRepository : IGymSessionRepository
    {

        private readonly DataContext _context;

        // Konstruktorn ser till att vi får tillgång till databasen (DataContext)
                public GymSessionRepository(DataContext context)
                {

                   _context = context;

                }

        public async Task<IEnumerable<GymSession>> GetAllAsync(string category)
        {
            var query = _context.GymSessions.AsQueryable();

            // Logik för att filtrera på kategori
            if (!string.IsNullOrEmpty(category))
                query = query.Where(x => x.Category == category);

            return await query.ToListAsync();
        }

        public async Task<GymSession?> GetByIdAsync(int id)
        {
            return await _context.GymSessions.FindAsync(id);
        }

        public async Task UpdateAsync(GymSession session)
        {
            // Här markerar vi sessionen som uppdaterad
            _context.GymSessions.Update(session);
            // Och här sparar vi ändringarna till databasen
            await _context.SaveChangesAsync();
        }



    }
}
