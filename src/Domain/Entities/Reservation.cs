using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public required string Reason { get; set; }
    public required string CurrentStatus { get; set; }
    public DateTime RequestDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid? SpaceId { get; set; }
    public Space? Space { get; set; }
    // Navegación Inversa
    public Voucher? Voucher { get; set; }

    // public ICollection<ReservationResource> ReservationResources { get; set; } = [];
    //public ICollection<ReservationHistory> ReservationHistories { get; set; } = [];
}