using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(r => r.Reason).IsRequired().HasMaxLength(500);
        builder.Property(r => r.CurrentStatus).IsRequired();

        builder.OwnsOne(r => r.Slot, slot =>
        {
            slot.Property(s => s.Date).HasColumnName("Date").IsRequired();
            slot.Property(s => s.StartTime).HasColumnName("StartTime").IsRequired();
            slot.Property(s => s.EndTime).HasColumnName("EndTime").IsRequired();
        });

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Space)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SpaceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Voucher)
            .WithOne(v => v.Reservation)
            .HasForeignKey<Voucher>(v => v.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}