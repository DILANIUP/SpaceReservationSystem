namespace SpaceReservationSystem.Domain.Primitives;

public class Role 
{
    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public  ICollection<User> Users { get; set; } = [];
}