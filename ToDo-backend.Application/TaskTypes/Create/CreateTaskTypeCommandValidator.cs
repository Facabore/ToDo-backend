using FluentValidation;

namespace ToDo_backend.Application.TaskTypes.Create;

public class CreateTaskTypeCommandValidator : AbstractValidator<CreateTaskTypeCommand>
{
    public CreateTaskTypeCommandValidator()
    {
        RuleFor(x => x.TaskType.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TaskType.ColorHex)
            .NotEmpty()
            .Matches("^#[0-9A-Fa-f]{6}$")
            .WithMessage("ColorHex must be a valid hex color code (e.g., #FFFFFF).");
    }
}