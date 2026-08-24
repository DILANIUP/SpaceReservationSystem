using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;

namespace SpaceReservationSystem.Infrastructure.Persistence.Repositories;

public class EmailLogRepository : IEmailLogRepository
{
    private readonly AppDbContext _context;

    public EmailLogRepository(AppDbContext context) => _context = context;

    public async Task<EmailLog?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.EmailLogs.FirstOrDefaultAsync(e => e.Id == id, ct);

    public void Add(EmailLog log) => _context.EmailLogs.Add(log);

    public void Update(EmailLog log) => _context.EmailLogs.Update(log);
}