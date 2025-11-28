using FluentValidation;

namespace ToDo_backend.Application.TaskTypes.Update;

public class UpdateTaskTypeCommandValidator : AbstractValidator<UpdateTaskTypeCommand>
{
    public UpdateTaskTypeCommandValidator()
    {
        RuleFor(x => x.TaskType.Id)
            .GreaterThan(0);

        RuleFor(x => x.TaskType.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TaskType.ColorHex)
            .NotEmpty()
            .Matches("^#[0-9A-Fa-f]{6}$")
            .WithMessage("ColorHex must be a valid hex color code (e.g., #FFFFFF).");
    }
}