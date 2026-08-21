using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Voucher : AuditableEntity
{
    public string PdfFilePath { get; private set; } = null!;
    public DateTime GenerationDate { get; private set; }
    public DateTime? EmailSentDate { get; private set; }

    public Guid ReservationId { get; private set; }
    public Reservation Reservation { get; private set; } = null!;


    private Voucher(Guid id, string pdfFilePath, Guid reservationId) : base(id)
    {
        PdfFilePath = pdfFilePath;
        ReservationId = reservationId;
        GenerationDate = DateTime.UtcNow;
    }

    private Voucher() { }

    public static Result<Voucher> Create(string pdfFilePath, Guid reservationId)
    {
        if (string.IsNullOrWhiteSpace(pdfFilePath))
            return Result.Failure<Voucher>(VoucherErrors.InvalidPath);

        if (reservationId == Guid.Empty)
            return Result.Failure<Voucher>(VoucherErrors.InvalidReservation);

        return new Voucher(Guid.NewGuid(), pdfFilePath.Trim(), reservationId);
    }

    public Result MarkAsSent()
    {
        EmailSentDate = DateTime.UtcNow;
        return Result.Success();
    }
}