namespace SpaceReservationSystem.Domain.Entities;

public class Voucher
{
    public Guid Id { get; set; }
    public required string PDFFilePath { get; set; }
    public DateTime GenerationDate { get; set; }
    public DateTime? EmailSentDate { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
}