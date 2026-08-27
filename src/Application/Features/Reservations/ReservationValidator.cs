using FluentValidation;

namespace SpaceReservationSystem.Application.Features.Reservations;

public class CreateReservationValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(500);
        RuleFor(x => x.SpaceId).NotNull()
            .WithMessage("SpaceId is required until Resource-based reservations are supported.");
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be greater than StartTime.");
    }
}