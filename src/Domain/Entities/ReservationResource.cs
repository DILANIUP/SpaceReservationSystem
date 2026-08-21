using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class ReservationResource : Entity
{
    public int RequestedQuantity { get; private set; }

    public Guid ReservationId { get; private set; }
    public Reservation Reservation { get; private set; } = null!;

    public Guid ResourceId { get; private set; }
    public Resource Resource { get; private set; } = null!;

private ReservationResource(Guid id, int requestedQuantity, Guid reservationId, Guid resourceId)
        : base(id)
    {
        RequestedQuantity = requestedQuantity;
        ReservationId = reservationId;
        ResourceId = resourceId;
    }

    private ReservationResource() { }

    public static Result<ReservationResource> Create(
        int requestedQuantity,
        Guid reservationId,
        Guid resourceId
    )
    {
        if (requestedQuantity <= 0)
            return Result.Failure<ReservationResource>(ReservationResourceErrors.InvalidQuantity);

        if (reservationId == Guid.Empty)
            return Result.Failure<ReservationResource>(ReservationResourceErrors.InvalidReservation);

        if (resourceId == Guid.Empty)
            return Result.Failure<ReservationResource>(ReservationResourceErrors.InvalidResource);

        return new ReservationResource(Guid.NewGuid(), requestedQuantity, reservationId, resourceId);
    }

    public Result UpdateQuantity(int requestedQuantity)
    {
        if (requestedQuantity <= 0)
            return Result.Failure(ReservationResourceErrors.InvalidQuantity);

        RequestedQuantity = requestedQuantity;
        return Result.Success();
    }
}