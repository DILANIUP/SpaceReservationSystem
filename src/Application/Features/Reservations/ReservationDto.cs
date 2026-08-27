namespace SpaceReservationSystem.Application.Features.Reservations;

public sealed record CreateReservationRequest(
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string Reason,
    Guid? SpaceId
);

public sealed record ReservationResponse(
    Guid Id,
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string Reason,
    string CurrentStatus,
    Guid UserId,
    Guid? SpaceId
);