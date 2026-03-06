using CoreFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Interfaces
{
    public interface IGymService
    {
        // Pass klasser
        Task<IEnumerable<GymPass>> GetAllPassesAsync();
        Task<GymPass?> GetPassByIdAsync(int id);


        //Booking 

        Task<bool> BookPassAsync(int passId, string userId);
        Task<IEnumerable<Booking>> GetUserBookingAsync(string userId);
        Task<bool> CancelBookingAsync(int bookingId, string userId);

        // Medlemskap
        Task<IEnumerable<Membership>> GetAllMembershipsAsync();

    }
}
