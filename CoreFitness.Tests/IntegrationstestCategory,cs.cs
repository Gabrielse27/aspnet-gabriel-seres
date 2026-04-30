using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Persistence.Contexts;
using CoreFitness.Web.Repositories;
using CoreFitness.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Assert = Xunit.Assert;

namespace CoreFitness.Tests
{
    public class IntegrationstestCategory_cs
    {


        [Fact]
        public async Task GetAllSessionsAsync_ShouldReturnCorrectCategory()
        {
            // 1. Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "CategoryTestDatabase_" + Guid.NewGuid().ToString())
                .Options;

            using (var context = new DataContext(options))
            {
                context.GymSessions.AddRange(
                    new GymSession { Id = 1, Name = "Yoga 1", Category = "Yoga", Description = "Desc" },
                    new GymSession { Id = 2, Name = "Power", Category = "Strength", Description = "Desc" }
                );
                await context.SaveChangesAsync();
            }

            // 2. Act
            using (var context = new DataContext(options))
            {
                var repository = new GymSessionRepository(context);
                var service = new TrainingService(repository);

                var result = await service.GetAllSessionsAsync("Yoga");

                // 3. Assert
                Assert.Single(result); // Kolla att vi bara fick 1 träff
                Assert.Equal("Yoga 1", result.First().Name); // Kolla att det var rätt pass
            }
        }




    }
}
