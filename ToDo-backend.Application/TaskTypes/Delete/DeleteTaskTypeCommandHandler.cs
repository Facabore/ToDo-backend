using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes.Delete;

internal sealed class DeleteTaskTypeCommandHandler : ICommandHandler<DeleteTaskTypeCommand>
{
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public DeleteTaskTypeCommandHandler(
        ITaskTypeRepository taskTypeRepository,
        ITodoTaskRepository todoTaskRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _taskTypeRepository = taskTypeRepository;
        _todoTaskRepository = todoTaskRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        DeleteTaskTypeCommand request,
        CancellationToken cancellationToken)
    {
        var taskType = await _taskTypeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (taskType is null) return Result.Failure(TaskErrors.TaskTypeNotFound);

        if (taskType.CreatedBy != _userContext.UserId) return Result.Failure(TaskErrors.TaskTypeNotOwnedByUser());

        var taskToMigrate = _todoTaskRepository.GetQueryable()
            .Where(t => t.TaskTypeId == request.Id);

        foreach (var task in taskToMigrate)
        {
            task.AssignDefaultId();
        }

        _taskTypeRepository.Delete(taskType);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}