using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Domain.Interfaces;

public interface IVoucherRepository
{
    Task<Voucher?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Voucher?> GetByReservationIdAsync(Guid reservationId, CancellationToken ct = default);
    void Add(Voucher voucher);
    void Update(Voucher voucher);
}