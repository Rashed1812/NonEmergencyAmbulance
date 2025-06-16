using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Identity_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.PhoneNumber)
                .HasMaxLength(20);

            builder.HasOne(d => d.User)
                .WithOne(u => u.DriverProfile)
                .HasForeignKey<Driver>(d => d.UserId);

            builder.HasMany(d => d.AssignedRequests)
                .WithOne(r => r.Driver)
                .HasForeignKey(r => r.DriverId);

            builder.HasMany(d => d.Trips)
                .WithOne(t => t.Driver)
                .HasForeignKey(t => t.DriverId);
        }
    }
}
