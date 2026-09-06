using FluentValidation;

namespace SpaceReservationSystem.Application.Features.Vouchers;

public class GenerateVoucherRequestValidator : AbstractValidator<GenerateVoucherRequest>
{
    public GenerateVoucherRequestValidator()
    {
        RuleFor(x => x.ReservationId).NotEmpty();
    }
}