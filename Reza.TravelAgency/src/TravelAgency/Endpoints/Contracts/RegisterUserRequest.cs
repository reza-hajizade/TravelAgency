using FluentValidation;

namespace TravelAgency.Endpoints.Contracts;

    public sealed record RegisterUserRequest(string FullName,string Email,string Password);
    

public sealed class RegisterValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MinimumLength(5)
            .WithMessage("Full name must be at least 5 characters long.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");
    }
}


