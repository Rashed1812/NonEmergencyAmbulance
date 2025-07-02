using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class TripConfiguration:IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(t => t.TripId);

            builder.Property(t => t.TripStatus)
                .HasConversion<string>();

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.Request)
                .WithOne(r => r.Trip)
                .HasForeignKey<Trip>(t => t.RequestId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
