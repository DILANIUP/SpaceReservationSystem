using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Domain.Entities;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasIndex(v => v.ReservationId).IsUnique();
    }
}