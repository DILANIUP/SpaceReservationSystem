using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;

namespace SpaceReservationSystem.Infrastructure.Persistence.Repositories;

public class EmailTemplateRepository : IEmailTemplateRepository
{
    private readonly AppDbContext _context;

    public EmailTemplateRepository(AppDbContext context) => _context = context;

    public async Task<EmailTemplate?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<EmailTemplate?> GetByCodeAsync(string code, CancellationToken ct = default)
        => await _context.EmailTemplates.FirstOrDefaultAsync(e => e.Code == code, ct);

    public void Add(EmailTemplate template) => _context.EmailTemplates.Add(template);

    public void Update(EmailTemplate template) => _context.EmailTemplates.Update(template);
}