using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Web.Repositories
{
    public interface IBookingRepository
    {

        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId);

        
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _context.Bookings
                .Include(b => b.GymPass)
                .Where(b => b.ApplicationUserId == userId)
                .ToListAsync();
        }



        

    }
 }