using System;
using System.Collections.Generic;
using System.Text;
using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace CoreFitness.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // Här definierar vi våra egna tabeller baserat på entiteterna i Domain-lagret
        public DbSet<GymPass> GymPasses { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        }
}
