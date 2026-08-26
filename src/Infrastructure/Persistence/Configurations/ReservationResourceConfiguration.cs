using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class ReservationResourceConfiguration : IEntityTypeConfiguration<ReservationResource>
{
    public void Configure(EntityTypeBuilder<ReservationResource> builder)
    {
        builder.Property(rr => rr.RequestedQuantity).IsRequired();

        builder.HasOne(rr => rr.Reservation)
            .WithMany(r => r.ReservationResources)
            .HasForeignKey(rr => rr.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rr => rr.Resource)
            .WithMany(res => res.ReservationResources)
            .HasForeignKey(rr => rr.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}