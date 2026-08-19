namespace SpaceReservationSystem.Domain.Entities;

public class Role 
{
    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public  ICollection<User> Users { get; set; } = [];
}