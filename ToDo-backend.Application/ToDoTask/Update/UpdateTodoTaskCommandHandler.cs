using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.Update;

internal sealed class UpdateTodoTaskCommandHandler : ICommandHandler<UpdateTodoTaskCommand, TodoTaskDto>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateTodoTaskCommandHandler(
        ITodoTaskRepository todoTaskRepository,
        ITaskTypeRepository taskTypeRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _todoTaskRepository = todoTaskRepository;
        _taskTypeRepository = taskTypeRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<TodoTaskDto>> Handle(
        UpdateTodoTaskCommand request,
        CancellationToken cancellationToken)
    {
        var todoTask = await _todoTaskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (todoTask is null) return Result.Failure<TodoTaskDto>(TaskErrors.TaskNotFound);

        if (todoTask.CreatedBy != _userContext.UserId) return Result.Failure<TodoTaskDto>(TaskErrors.TaskNotOwnedByUser());

        var taskType = await _taskTypeRepository.GetByIdAsync(request.TodoTask.TaskTypeId, cancellationToken);

        if (taskType is null) return Result.Failure<TodoTaskDto>(TaskErrors.TaskTypeNotFound);

        todoTask.UpdateDetails(
            request.TodoTask.Title,
            request.TodoTask.Description,
            request.TodoTask.TaskTypeId,
            _dateTimeProvider.UtcNow);

        _todoTaskRepository.Update(todoTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(todoTask.ToDto());
    }
}