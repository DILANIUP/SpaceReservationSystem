using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class EmailLogConfiguration : IEntityTypeConfiguration<EmailLog>
{
    public void Configure(EntityTypeBuilder<EmailLog> builder)
    {
        builder.Property(e => e.ToEmail).IsRequired().HasMaxLength(256);
        builder.Property(e => e.ErrorMessage).HasMaxLength(1000);

        builder.HasOne(e => e.Reservation)
            .WithMany()
            .HasForeignKey(e => e.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Alert)
            .WithMany()
            .HasForeignKey(e => e.AlertId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Template)
            .WithMany(t => t.EmailLogs)
            .HasForeignKey(e => e.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}