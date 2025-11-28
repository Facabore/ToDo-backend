using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes.Update;

internal sealed class UpdateTaskTypeCommandHandler : ICommandHandler<UpdateTaskTypeCommand, TaskTypeDto>
{
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateTaskTypeCommandHandler(
        ITaskTypeRepository taskTypeRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _taskTypeRepository = taskTypeRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<TaskTypeDto>> Handle(
        UpdateTaskTypeCommand request,
        CancellationToken cancellationToken)
    {
        var taskType = await _taskTypeRepository.GetByIdAsync(request.TaskType.Id, cancellationToken);

        if (taskType is null) return Result.Failure<TaskTypeDto>(TaskErrors.TaskTypeNotFound);

        if (taskType.CreatedBy != _userContext.UserId)
        {
            return Result.Failure<TaskTypeDto>(TaskErrors.TaskTypeNotOwnedByUser());
        }

        taskType.Update(request.TaskType.Name, request.TaskType.ColorHex);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(taskType.ToDto());
    }
}