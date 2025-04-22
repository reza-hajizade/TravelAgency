using FluentValidation;

namespace TravelAgency.Endpoints.Contracts;

    public sealed record UpdateHotelRequest(string Name, decimal PricePerNight);
    

public sealed class UpdateHotelRequestValidator : AbstractValidator<UpdateHotelRequest>
{

    public UpdateHotelRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(p => p.PricePerNight)
            .NotEmpty()
            .WithMessage("PricePerNight is required.")
              .GreaterThan(0)
            .WithMessage("Price per night must be greater than 0.");


    }

}

