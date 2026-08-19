namespace SpaceReservationSystem.Domain.Entities;

public class Career
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    public ICollection<User> Users { get; set; } = [];
}  