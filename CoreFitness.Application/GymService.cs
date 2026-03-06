using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application
{
    public class GymService : IGymService
    {
        // Vi lämnar denna tom en kort stund tills vi har skapat vår DbContext i Infrastructure!

        public async Task<IEnumerable<GymPass>> GetAllPassesAsync()
        {
            // Här hämtar vi pass från databasen senare
            return new List<GymPass>();
        }

        public async Task<bool> BookPassAsync(int passId, string userId)
        {
            // Här lägger vi in logiken för att förhindra dubbelbokning (Krav för G/VG)
            return true;
        }
        // ... resten av metoderna implementeras på samma sätt
        public Task<IEnumerable<Membership>> GetAllMembershipsAsync() => throw new NotImplementedException();
        public Task<GymPass?> GetPassByIdAsync(int id) => throw new NotImplementedException();
        public Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId) => throw new NotImplementedException();
        public Task<bool> CancelBookingAsync(int bookingId, string userId) => throw new NotImplementedException();

        public Task<IEnumerable<Booking>> GetUserBookingAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
