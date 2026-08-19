namespace SpaceReservationSystem.Domain.Entities;

public class EmailTemplate
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }

    public ICollection<EmailLog> EmailLogs { get; set; } = [];
}