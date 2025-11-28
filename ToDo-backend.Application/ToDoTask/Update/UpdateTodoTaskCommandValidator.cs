using FluentValidation;

namespace ToDo_backend.Application.ToDoTask.Update;

public class UpdateTodoTaskCommandValidator : AbstractValidator<UpdateTodoTaskCommand>
{
    public UpdateTodoTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.TodoTask.Title)
            .MaximumLength(200);

        RuleFor(x => x.TodoTask.Description)
            .MaximumLength(1000);
    }
}