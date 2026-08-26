using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.ValueObjects;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default);
    void Add(User user);
    void Update(User user);
}