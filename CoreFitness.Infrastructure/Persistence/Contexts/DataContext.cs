



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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly); 
        } 
        public DbSet<MemberEntity> Members => Set<MemberEntity>();

    }
}
