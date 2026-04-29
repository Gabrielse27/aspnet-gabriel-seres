using CoreFitness.Domain.Entities;



namespace CoreFitness.Web.Repositories
{
    public interface IGymSessionRepository
    {
        // Här definierar vi "kontraktet" – vilka metoder som måste finnas
                Task<IEnumerable<GymSession>> GetAllAsync(string category);
                Task<GymSession?> GetByIdAsync(int id);
                Task UpdateAsync(GymSession session);


    }
}
