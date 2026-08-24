using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;

namespace SpaceReservationSystem.Infrastructure.Persistence.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context) => _context = context;

    public async Task<Reservation?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<Reservation?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
        => await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Space)
            .Include(r => r.Voucher)
            .Include(r => r.ReservationResources)
            .Include(r => r.ReservationHistories)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public void Add(Reservation reservation) => _context.Reservations.Add(reservation);

    public void Update(Reservation reservation) => _context.Reservations.Update(reservation);
}