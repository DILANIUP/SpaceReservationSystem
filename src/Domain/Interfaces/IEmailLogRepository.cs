using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface IEmailLogRepository
{
    Task<EmailLog?> GetByIdAsync(Guid id, CancellationToken ct = default);
    void Add(EmailLog log);
    void Update(EmailLog log);
}