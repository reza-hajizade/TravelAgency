using FluentValidation;

namespace TravelAgency.Endpoints.Contracts;

    public sealed record CreateHotelRequest(
        string Name,
        string PhoneNumber,
        string Address,
        string City,
        string Description,
        int Stars,
        decimal PricePerNight,
        bool HasWifi,
        bool HasParking
    );



public sealed class CreateHotelRequestValidator : AbstractValidator<CreateHotelRequest>
{
    public CreateHotelRequestValidator()
    {
        RuleFor(h => h.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(h => h.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^[0-9]{11}$")
            .WithMessage("Phone number must be a valid format.");
        RuleFor(h => h.Address)
            .NotEmpty()
            .WithMessage("Address is required.");
        RuleFor(h => h.City)
            .NotEmpty()
            .WithMessage("City is required.");
        RuleFor(h => h.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
        RuleFor(h => h.Stars)
            .InclusiveBetween(1, 5)
            .WithMessage("Stars must be between 1 and 5.");
        RuleFor(h => h.PricePerNight)
            .GreaterThan(0)
            .WithMessage("Price per night must be greater than 0.");
        RuleFor(h => h.HasWifi)
            .NotNull()
            .WithMessage("HasWifi is required.");
        RuleFor(h => h.HasParking)
            .NotNull()
            .WithMessage("HasParking is required.");
    }
}


