using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface ICareerRepository
{
    Task<Career?> GetByIdAsync(Guid id, CancellationToken ct = default);
    void Add(Career career);
    void Update(Career career);
}