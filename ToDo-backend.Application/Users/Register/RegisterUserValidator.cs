using FluentValidation;

namespace ToDo_backend.Application.Users.Register;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    private const int MinPasswordLength = 5;
    public RegisterUserValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(MinPasswordLength);
    }
}