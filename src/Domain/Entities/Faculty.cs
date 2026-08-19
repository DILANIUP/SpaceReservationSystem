namespace SpaceReservationSystem.Domain.Primitives;

public class Faculty
{
    public Guid Id { get; set; }
    public required string name { get; set; }
    public ICollection<Career> Careers { get; set; } = [];
}