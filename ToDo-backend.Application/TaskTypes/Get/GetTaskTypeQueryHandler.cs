using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes.Get;

internal sealed class GetTaskTypeQueryHandler : IQueryHandler<GetTaskTypeQuery, TaskTypeDto>
{
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUserContext _userContext;

    public GetTaskTypeQueryHandler(ITaskTypeRepository taskTypeRepository, IUserContext userContext)
    {
        _taskTypeRepository = taskTypeRepository;
        _userContext = userContext;
    }

    public async Task<Result<TaskTypeDto>> Handle(
        GetTaskTypeQuery request,
        CancellationToken cancellationToken)
    {
        var taskType = await _taskTypeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (taskType is null) return Result.Failure<TaskTypeDto>(TaskErrors.TaskTypeNotFound);

        if (taskType.CreatedBy != _userContext.UserId)
        {
            return Result.Failure<TaskTypeDto>(TaskErrors.TaskTypeNotOwnedByUser());
        }

        return Result.Success(taskType.ToDto());
    }
}