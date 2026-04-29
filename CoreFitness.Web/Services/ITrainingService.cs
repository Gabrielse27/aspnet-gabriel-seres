using CoreFitness.Domain.Entities;

namespace CoreFitness.Web.Services
{
    public interface ITrainingService
    {

        Task<IEnumerable<GymSession>> GetAllSessionsAsync(string category);
        Task BookSessionAsync(int sessionId, string userId);
        Task UnBookSessionAsync(int sessionId);
    }
}
