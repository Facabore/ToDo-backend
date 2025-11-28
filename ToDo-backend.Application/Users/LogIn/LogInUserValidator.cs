using FluentValidation;
using ToDo_backend.Application.Users.Register;

namespace ToDo_backend.Application.Users.LogIn;

public sealed class LogInUserValidator : AbstractValidator<RegisterUserCommand>
{
    private const int MinPasswordLength = 5;
    public LogInUserValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(MinPasswordLength);
    }
}