using FluentValidation;

namespace TravelAgency.Endpoints.Contracts;

    public sealed record LoginUserRequest(string username,string password); 


public sealed class LoginValidator : AbstractValidator<LoginUserRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(5)
            .WithMessage("Username must be at least 5 characters long.");
        RuleFor(x => x.password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");
    }
}



