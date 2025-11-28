using FluentValidation;

namespace ToDo_backend.Application.ToDoTask.Get;

public class GetTodoTaskQueryValidator : AbstractValidator<GetTodoTaskQuery>
{
    public GetTodoTaskQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}