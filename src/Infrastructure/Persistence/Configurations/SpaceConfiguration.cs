using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class SpaceConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.Property(s => s.Name).IsRequired().HasMaxLength(150);
        builder.Property(s => s.Type).IsRequired();
        builder.Property(s => s.Location).IsRequired().HasMaxLength(250);
    }
}