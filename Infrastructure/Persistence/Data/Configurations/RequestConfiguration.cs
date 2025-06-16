using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(r => r.RequestId);

            builder.Property(r => r.Status)
                   .HasConversion<string>();

            builder.HasOne(r => r.Patient)
                   .WithMany(p => p.Requests)
                   .HasForeignKey(r => r.PatientId);

            builder.HasOne(r => r.Driver)
                   .WithMany(d => d.AssignedRequests)
                   .HasForeignKey(r => r.DriverId);

            builder.HasOne(r => r.Nurse)
                   .WithMany(n => n.AssignedRequests)
                   .HasForeignKey(r => r.NurseId);

            builder.HasOne(r => r.Trip)
                   .WithOne(t => t.Request)
                   .HasForeignKey<Trip>(t => t.RequestId);
        }
    }
}
