using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes.GetMyTaskTypes;

internal sealed class GetMyTaskTypesQueryHandler : IQueryHandler<GetMyTaskTypesQuery, IEnumerable<TaskTypeDto>>
{
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUserContext _userContext;

    public GetMyTaskTypesQueryHandler(ITaskTypeRepository taskTypeRepository, IUserContext userContext)
    {
        _taskTypeRepository = taskTypeRepository;
        _userContext = userContext;
    }

    public async Task<Result<IEnumerable<TaskTypeDto>>> Handle(
        GetMyTaskTypesQuery request,
        CancellationToken cancellationToken)
    {
        var taskTypes = await _taskTypeRepository.GetByCreatedByAsync(_userContext.UserId, cancellationToken);

        var dtos = taskTypes.Select(tt => tt.ToDto());

        return Result.Success(dtos);
    }
}