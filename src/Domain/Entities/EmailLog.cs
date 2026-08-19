namespace SpaceReservationSystem.Domain.Entities;

public class EmailLog
{
    public Guid Id { get; set; }
    public required string ToEmail { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsSent { get; set; }
    public string? ErrorMessage { get; set; }

    public Guid? ReservationId { get; set; }
    public Reservation? Reservation { get; set; }

    public Guid? AlertId { get; set; }
    public Alert? Alert { get; set; }

    public Guid TemplateId { get; set; }
    public EmailTemplate Template { get; set; } = null!;
}