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
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Certification).HasMaxLength(100);

            builder.HasOne(n => n.User)
                .WithOne(u => u.NurseProfile)
                .HasForeignKey<Nurse>(n => n.UserId);
        }
    }
}
