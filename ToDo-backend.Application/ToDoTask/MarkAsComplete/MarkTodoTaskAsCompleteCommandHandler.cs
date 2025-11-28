using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.MarkAsComplete;

internal sealed class MarkTodoTaskAsCompleteCommandHandler : ICommandHandler<MarkTodoTaskAsCompleteCommand, TodoTaskDto>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public MarkTodoTaskAsCompleteCommandHandler(
        ITodoTaskRepository todoTaskRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _todoTaskRepository = todoTaskRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<TodoTaskDto>> Handle(
        MarkTodoTaskAsCompleteCommand request,
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

        todoTask.MarkAsComplete(_dateTimeProvider.UtcNow);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(todoTask.ToDto());
    }
}