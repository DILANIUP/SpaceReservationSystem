namespace SpaceReservationSystem.Domain.Entities;

public class Alert
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public bool IsResolved { get; set; }

    public Guid? ResourceId { get; set; }
    public Resource? Resource { get; set; }

    public Guid? SpaceId { get; set; }
    public Space? Space { get; set; }
}