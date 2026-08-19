using System.Collections.Generic;

namespace SpaceReservationSystem.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string phone { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public Guid? CareerId { get; set; }
    public Career? Career { get; set; } = null!; //* es nullable porque no todos los roles necesariamente tienen
    // public ICollection<Reservation> Reservations { get; set; } = [];
    public ICollection<ReservationHistory> ReservationHistories { get; set; } = [];
}