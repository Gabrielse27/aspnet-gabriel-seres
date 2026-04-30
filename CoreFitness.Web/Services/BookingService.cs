using Microsoft.EntityFrameworkCore;
using CoreFitness.Domain.Entities;

using CoreFitness.Infrastructure;
using CoreFitness.Infrastructure.Persistence.Contexts;

namespace CoreFitness.Web.Services
{
    public class BookingService : IBookingService
    {
        private readonly DataContext _context; // Här definierar vi variabeln

        // Constructor: Här ser vi till att databasen kopplas in
        public BookingService(DataContext context)
        {
            _context = context;
        }




        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {

            var sessions = await _context.GymSessions
           .Where(s => s.BookedByUserId == userId)
           .ToListAsync();



            // 2. Omvandla till Booking-objekt
            var bookings = sessions.Select(s => new Booking
            {
                Id = s.Id,
                GymPassId = s.Id,
                // Kontrollera här: s.StartTime är namnet på datumet i GymSession
                BookedAt = s.StartTime,
                SessionName = s.Name,
                StartTime = s.StartTime,
                EndTime = s.EndTime
            }).ToList();

            return bookings;
        }
    }
}