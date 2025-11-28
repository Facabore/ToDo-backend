using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.Create;

internal sealed class CreateTodoTaskCommandHandler : ICommandHandler<CreateTodoTaskCommand, TodoTaskDto>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateTodoTaskCommandHandler(
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
        CreateTodoTaskCommand request,
        CancellationToken cancellationToken)
    {
        var taskType = await _taskTypeRepository.GetByIdAsync(request.TodoTask.TaskTypeId, cancellationToken);

        if (taskType is null)
        {
            return Result.Failure<TodoTaskDto>(TaskErrors.TaskTypeNotFound);
        }

        var todoTask = request.TodoTask.ToEntity(taskType, _userContext.UserId, _dateTimeProvider.UtcNow);

        _todoTaskRepository.Add(todoTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(todoTask.ToDto());
    }
}