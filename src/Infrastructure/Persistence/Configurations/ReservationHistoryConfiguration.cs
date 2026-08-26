using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class ReservationHistoryConfiguration : IEntityTypeConfiguration<ReservationHistory>
{
    public void Configure(EntityTypeBuilder<ReservationHistory> builder)
    {
        builder.Property(rh => rh.Justification).IsRequired().HasMaxLength(500);

        builder.HasOne(rh => rh.Reservation)
            .WithMany(r => r.ReservationHistories)
            .HasForeignKey(rh => rh.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rh => rh.ChangedBy)
            .WithMany(u => u.ReservationHistories)
            .HasForeignKey(rh => rh.ChangedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}