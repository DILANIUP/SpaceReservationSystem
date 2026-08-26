using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Persistence.Configurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Subject).IsRequired().HasMaxLength(250);
        builder.Property(e => e.Body).IsRequired();

        builder.HasIndex(e => e.Code).IsUnique();
    }
}