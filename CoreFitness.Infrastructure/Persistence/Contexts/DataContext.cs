
using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Persistence.Entities.Members;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Persistence.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly); 
        } 
        public DbSet<MemberEntity> Members => Set<MemberEntity>();
        public DbSet<ContactRequestEntity> ContactRequests => Set<ContactRequestEntity>();

        public DbSet<ContactRequestEntity> Messages { get; set; }
    }
}
