namespace SpaceReservationSystem.Domain.Entities;

public class ReservationHistory
{
    public Guid Id { get; set; }
    public required string PreviousStatus { get; set; }
    public required string NewStatus { get; set; }
    public required string Justification { get; set; }
    public DateTime ChangeDate { get; set; }

    public Guid ChangedById { get; set; }
    public User ChangedBy { get; set; } = null!;

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
}