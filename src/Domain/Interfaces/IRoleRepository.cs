using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Enums;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Role?> GetByCodeAsync(RoleCode code, CancellationToken ct = default);
    void Add(Role role);
    void Update(Role role);
}