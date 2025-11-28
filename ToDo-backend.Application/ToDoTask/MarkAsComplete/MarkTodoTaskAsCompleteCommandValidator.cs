using FluentValidation;

namespace ToDo_backend.Application.ToDoTask.MarkAsComplete;

public class MarkTodoTaskAsCompleteCommandValidator : AbstractValidator<MarkTodoTaskAsCompleteCommand>
{
    public MarkTodoTaskAsCompleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}