using CoreFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreFitness.Application.Interfaces
{
    public interface IGymService
    {
        // Pass (Klasser)
        Task<IEnumerable<GymPass>> GetAllPassesAsync();
        Task<GymPass?> GetPassByIdAsync(int id);

        // Bokning
        Task<bool> BookPassAsync(int passId, string userId);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
        Task<bool> CancelBookingAsync(int bookingId, string userId);

        // Medlemskap
        Task<IEnumerable<Membership>> GetAllMembershipsAsync();

    }
}
