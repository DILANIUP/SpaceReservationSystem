using FluentValidation;

namespace SpaceReservationSystem.Application.Features.Reservations;

public class CreateReservationValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.Reason).NotEmpty().MaximumLength(300);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);

        RuleForEach(x => x.Resources).ChildRules(r =>
        {
            r.RuleFor(x => x.Quantity).GreaterThan(0);
            r.RuleFor(x => x.ResourceId).NotEmpty();
        });
    }
}