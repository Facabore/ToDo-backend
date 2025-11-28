using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes.Create;

internal sealed class CreateTaskTypeCommandHandler : ICommandHandler<CreateTaskTypeCommand, TaskTypeDto>
{
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskTypeCommandHandler(
        ITaskTypeRepository taskTypeRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _taskTypeRepository = taskTypeRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TaskTypeDto>> Handle(
        CreateTaskTypeCommand request,
        CancellationToken cancellationToken)
    {
        var taskType = request.TaskType.ToEntity(_userContext.UserId);

        _taskTypeRepository.Add(taskType);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(taskType.ToDto());
    }
}