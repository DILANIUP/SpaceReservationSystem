using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;

namespace SpaceReservationSystem.Infrastructure.Persistence.Repositories;

public class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _context;

    public VoucherRepository(AppDbContext context) => _context = context;

    public async Task<Voucher?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == id, ct);

    public void Add(Voucher voucher) => _context.Vouchers.Add(voucher);

    public void Update(Voucher voucher) => _context.Vouchers.Update(voucher);
}