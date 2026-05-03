using CoreFitness.Web.Repositories;
using CoreFitness.Domain.Entities;

namespace CoreFitness.Web.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IGymSessionRepository _repository;

        public TrainingService(IGymSessionRepository repository) => _repository = repository;

        public async Task<IEnumerable<GymSession>> GetAllSessionsAsync(string category)
            => await _repository.GetAllAsync(category);

        public async Task GetUserBookingsAsync(int sessionId, string userId)
        {
            // Vi hämtar passet först för att få namnet och tiderna till vår mappning
            var session = await _repository.GetByIdAsync(sessionId);

            if (session == null)
            {
                // NY LOGIK: Vi skapar en bokning istället för att ändra passet
                var newBooking = new Booking
                {
                    GymPassId = sessionId,
                    ApplicationUserId = userId,
                    SessionName = session.Name, // Här mappar vi namnet från passet
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    BookedAt = DateTime.Now
                };

                // Här använder vi vårt nya IBookingRepository (som vi behöver lägga till)
                await _repository.UpdateAsync(session);
            }


        }


        public async Task BookSessionAsync(int sessionId, string userId)
        {
            var session = await _repository.GetByIdAsync(sessionId);
            if (session != null)
            {
                session.BookedByUserId = userId;
                await _repository.UpdateAsync(session);
            }
        }






        public async Task UnBookSessionAsync(int sessionId)
        {
            var session = await _repository.GetByIdAsync(sessionId);
            if (session != null)
            {
                session.BookedByUserId = null;
                await _repository.UpdateAsync(session);
            }
        }
    }
}