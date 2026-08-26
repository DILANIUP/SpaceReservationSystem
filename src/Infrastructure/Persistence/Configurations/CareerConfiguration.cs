using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class CareerConfiguration : IEntityTypeConfiguration<Career>
{
    public void Configure(EntityTypeBuilder<Career> builder)
    {
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);

        builder.HasOne(c => c.Faculty)
            .WithMany(f => f.Careers)
            .HasForeignKey(c => c.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}