using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Application.Features.Reservations;

public class ReservationService(
    IReservationRepository reservationRepository,
    ISpaceRepository spaceRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateReservationRequest> validator
)
{
    public async Task<Result<ReservationResponse>> CreateAsync(CreateReservationRequest request, Guid userId, CancellationToken ct)
    {
        var validation = await validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            var f = validation.Errors[0];
            return Result.Failure<ReservationResponse>(Error.Validation(f.PropertyName, f.ErrorMessage));
        }

        var reservationResult = Domain.Entities.Reservation.Create(
            request.Date, request.StartTime, request.EndTime, request.Reason, userId, request.SpaceId
        );

        if(reservationResult.IsFailure)
            return Result.Failure<ReservationResponse>(reservationResult.Error);

        var reservation = reservationResult.Value;

        if(request.SpaceId is not null)
        {
            var space = await spaceRepository.GetByIdAsync(request.SpaceId.Value, ct);
            if(space is null)
                return Result.Failure<ReservationResponse>(
                    Error.NotFound("Space", request.SpaceId.Value.ToString())
                );

            if(!space.IsActive)
                return Result.Failure<ReservationResponse>(
                    Error.Conflict("Space", "El espacio está inactivo y no puede reservarse.")
                );

            var existing = await reservationRepository.GetActiveBySpaceAndDateAsync
                (request.SpaceId.Value, request.Date, ct);

            if(existing.Any(r => r.Slot.Overlaps(reservation.Slot)))
                return Result.Failure<ReservationResponse>(ReservationErrors.SlotAlreadyTaken);
        }

        reservationRepository.Add(reservation);
        await unitOfWork.SaveChangesAsync(ct);

        return new ReservationResponse(
            reservation.Id,
            reservation.Slot.Date,
            reservation.Slot.StartTime,
            reservation.Slot.EndTime,
            reservation.Reason,
            reservation.CurrentStatus.ToString(),
            reservation.UserId,
            reservation.SpaceId
        );
    }
}