using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.Get;

internal sealed class GetTodoTaskQueryHandler : IQueryHandler<GetTodoTaskQuery, TodoTaskDto>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUserContext _userContext;

    public GetTodoTaskQueryHandler(ITodoTaskRepository todoTaskRepository, IUserContext userContext)
    {
        _todoTaskRepository = todoTaskRepository;
        _userContext = userContext;
    }

    public async Task<Result<TodoTaskDto>> Handle(
        GetTodoTaskQuery request,
        CancellationToken cancellationToken)
    {
        var todoTask = await _todoTaskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (todoTask is null)
        {
            return Result.Failure<TodoTaskDto>(TaskErrors.TaskNotFound);
        }

        if (todoTask.CreatedBy != _userContext.UserId)
        {
            return Result.Failure<TodoTaskDto>(TaskErrors.TaskNotOwnedByUser());
        }

        return Result.Success(todoTask.ToDto());
    }
}