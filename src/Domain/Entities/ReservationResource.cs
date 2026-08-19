namespace SpaceReservationSystem.Domain.Entities;

public class ReservationResource
{
    public Guid Id { get; set; }
    public int RequestedQuantity { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;

    public Guid ResourceId { get; set; }
    public Resource Resource { get; set; } = null!;
}