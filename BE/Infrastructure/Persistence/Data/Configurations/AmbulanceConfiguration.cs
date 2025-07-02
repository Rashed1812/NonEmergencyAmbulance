using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Ambulance_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class AmbulanceConfiguration : IEntityTypeConfiguration<Ambulance>
    {
        public void Configure(EntityTypeBuilder<Ambulance> builder)
        {
            builder.HasKey(a => a.AmbulanceId);
            builder.Property(a => a.PlateNumber).HasMaxLength(20);
            builder.Property(a => a.CurrentLocation).HasMaxLength(100);

            builder.Property(a => a.Status).HasConversion<string>();
            builder.Property(a => a.Type).HasConversion<string>();

            builder.HasOne(a => a.Driver)
                   .WithMany(d => d.Ambulances)
                   .HasForeignKey(a => a.DriverId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
