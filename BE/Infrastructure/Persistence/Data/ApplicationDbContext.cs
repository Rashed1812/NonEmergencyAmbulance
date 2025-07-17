using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Ambulance_Module;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ambulance> Ambulances { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
    }
}
