using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using CoreFitness.Web.Repositories; // För att hitta GymSessionRepository
using CoreFitness.Web.Services;
using Xunit;
using Assert = Xunit.Assert;



namespace CoreFitness.Tests
{
    
    public class GymUnbookIntegrationstest


    {

        [Fact]
        public async Task UnBookSessionAsync_ShouldClearUserId_WhenSessionIsUnbooked()
        {
            // 1. Arrange - Sätt upp en InMemory-databas och lägg till ett bokat pass
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "UnbookTestDatabase_" + Guid.NewGuid().ToString())
            .Options;

            using (var context = new DataContext(options))
            {
                var session = new GymSession
                {
                    Id = 10,
                    Name = "Yoga Flow",
                    Category = "Yoga",
                    Description = "A relaxing yoga session",
                    BookedByUserId = "test-user-123", // Passet är redan bokat
                    StartTime = DateTime.Now.AddDays(1)
                };
                context.GymSessions.Add(session);
                await context.SaveChangesAsync();
            }

            // 2. Act - Kör metoden som ska ta bort bokningen
            using (var context = new DataContext(options))
            {
                // Vi skapar ett repository och en service som pratar med vår InMemory-db
                var repository = new GymSessionRepository(context);
                var service = new TrainingService(repository);

                await service.UnBookSessionAsync(10); // Anropar avbokningen
            }


            using (var context = new DataContext(options))
            {
                var updatedSession = await context.GymSessions.FindAsync(10);

                Assert.NotNull(updatedSession);
                Assert.Null(updatedSession.BookedByUserId); // Kontrollera att ID:t är raderat[cite: 2]
            }
        }
    }

}