using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface IFacultyRepository
{
    Task<Faculty?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Faculty>> GetAllAsync(CancellationToken ct = default);
    void Add(Faculty faculty);
    void Update(Faculty faculty);
}