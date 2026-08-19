namespace SpaceReservationSystem.Domain.Entities;

public class Space
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int Capacity { get; set; }
    public required string Location { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Reservation> Reservations { get; set; } = [];
    public ICollection<Alert> Alerts { get; set; } = [];
}