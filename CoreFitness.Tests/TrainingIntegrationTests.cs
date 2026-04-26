using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting; // Används för MSTest (samma som i UserTests)
using CoreFitness.Domain.Entities;
using System.Threading.Tasks;
using CoreFitness.Infrastructure.Persistence;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using CoreFitness.Infrastructure.Persistence.Contexts;



[TestClass]
public class TrainingIntegrationTests
{
    [TestMethod]
    public async Task BookSession_ShouldUpdateDatabase_WhenSessionExists()
    {
        //  Arrange: Setup InMemory-databas
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "Test_CoreFitness_DB_" + System.Guid.NewGuid()) // Unikt namn för varje test
            .Options;

        // Lägg in testdata
        using (var context = new DataContext(options))
        {
            var randomId = new Random().Next(1, 10000);

            context.GymSessions.Add(new GymSession { Name = "Yoga",Category = "Fitness", Description= "Ett Pass" });
            context.SaveChanges();
        }

        // 2. Act: Utför bokningen
        using (var context = new DataContext(options))
        {
            // Här simulerar vi en bokning
            var session = await context.GymSessions.FindAsync(1);
            session.BookedByUserId = "test-user-123";
            await context.SaveChangesAsync();
        }

        // 3. Assert: Kontrollera att ändringen sparades
        using (var context = new DataContext(options))
        {
            var updatedSession = await context.GymSessions.FindAsync(1);
            Assert.IsNotNull(updatedSession);
            Assert.AreEqual("test-user-123", updatedSession.BookedByUserId);
        }
    }
}


