using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface IEmailTemplateRepository
{
    Task<EmailTemplate?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<EmailTemplate?> GetByCodeAsync(string code, CancellationToken ct = default);
    void Add(EmailTemplate template);
    void Update(EmailTemplate template);
}