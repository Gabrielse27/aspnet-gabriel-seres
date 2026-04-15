
using CoreFitness.Domain.Agregates.Members;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using User = CoreFitness.Domain.Identity.User;


namespace CoreFitness.Infrastructure.Persistence.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Gör det tydligt att en session kan vara bokad av en användare
    modelBuilder.Entity<GymSession>()
        .HasOne(g => g.BookedByUser)
        .WithMany() // Eller .WithMany(u => u.BookedSessions) om du lägger till en lista i User-entiteten
        .HasForeignKey(g => g.BookedByUserId);




            // Seed data för Memberships
            modelBuilder.Entity<Memberships>().HasData(
                new Memberships
                {
                    Id = 1,
                    Type = "Standard",
                    Price = 495m,
                    Description = "Access to all gym locations, Standard equipment, Locker room access"
                },
                new Memberships
                {
                    Id = 2,
                    Type = "Premium",
                    Price = 595m,
                    Description = "All Standard benefits, Personal trainer sessions, Nutrition plan, Sauna access"
                }
            );







            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly); 
        } 
        public DbSet<MemberEntity> Members => Set<MemberEntity>();
        public DbSet<ContactRequestEntity> ContactRequests => Set<ContactRequestEntity>();

        public DbSet<ContactRequestEntity> Messages { get; set; }

        public DbSet<GymSession> GymSessions { get; set; }

       
    }
}
