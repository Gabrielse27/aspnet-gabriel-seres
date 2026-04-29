using CoreFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreFitness.Web.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
    }
}