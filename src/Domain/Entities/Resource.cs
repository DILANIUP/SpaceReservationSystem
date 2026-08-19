namespace SpaceReservationSystem.Domain.Entities;

public class Resource
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int AvailableQuantity { get; set; }
    public bool Status { get; set; }

    public ICollection<ReservationResource> ReservationResources { get; set; } = [];
    public ICollection<Alert> Alerts { get; set; } = [];
}