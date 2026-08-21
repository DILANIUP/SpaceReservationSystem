using SpaceReservationSystem.Domain.Enums;
using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class ReservationHistory : Entity
{
    public ReservationStatus PreviousStatus { get; private set; } // !important: revisar el enum si usar ese mismo o crear uno independiente para los status del historial
    public ReservationStatus NewStatus { get; private set; }
    public string Justification { get; private set; } = null!;
    public DateTime ChangeDate { get; private set; }

    public Guid ChangedById { get; private set; }
    public User ChangedBy { get; private set; } = null!;

    public Guid ReservationId { get; private set; }
    public Reservation Reservation { get; private set; } = null!;

    private ReservationHistory(
        Guid id,
        ReservationStatus previousStatus,
        ReservationStatus newStatus,
        string justification,
        Guid changedById,
        Guid reservationId
    ) : base(id)
    {
        PreviousStatus = previousStatus;
        NewStatus = newStatus;
        Justification = justification;
        ChangeDate = DateTime.UtcNow;
        ChangedById = changedById;
        ReservationId = reservationId;
    }

    private ReservationHistory() { }

    public static Result<ReservationHistory> Create(
        ReservationStatus previousStatus,
        ReservationStatus newStatus,
        string justification,
        Guid changedById,
        Guid reservationId
    )
    {
        if (string.IsNullOrWhiteSpace(justification))
            return Result.Failure<ReservationHistory>(ReservationHistoryErrors.InvalidJustification);

        if (changedById == Guid.Empty)
            return Result.Failure<ReservationHistory>(ReservationHistoryErrors.InvalidChangedBy);

        if (reservationId == Guid.Empty)
            return Result.Failure<ReservationHistory>(ReservationHistoryErrors.InvalidReservation);

        return new ReservationHistory(Guid.NewGuid(), previousStatus, newStatus, justification.Trim(), changedById, reservationId);
    }


}