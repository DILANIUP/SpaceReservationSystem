namespace SpaceReservationSystem.Domain.Entities;

public class Faculty
{
    public Guid Id { get; set; }
    public required string name { get; set; }
    public ICollection<Career> Careers { get; set; } = [];
}