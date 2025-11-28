using FluentValidation;

namespace ToDo_backend.Application.ToDoTask.Create;

public class CreateTodoTaskCommandValidator : AbstractValidator<CreateTodoTaskCommand>
{
    public CreateTodoTaskCommandValidator()
    {
        RuleFor(x => x.TodoTask.Title)
            .MaximumLength(200);

        RuleFor(x => x.TodoTask.Description)
            .MaximumLength(1000);
    }
}