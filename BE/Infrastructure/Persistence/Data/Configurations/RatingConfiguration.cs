using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Trip)
                .WithMany()
                .HasForeignKey(r => r.TripId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 