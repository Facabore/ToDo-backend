using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes.GetAll;

internal sealed class GetTaskTypesQueryHandler : IQueryHandler<GetTaskTypesQuery, IEnumerable<TaskTypeDto>>
{
    private readonly ITaskTypeRepository _taskTypeRepository;

    public GetTaskTypesQueryHandler(ITaskTypeRepository taskTypeRepository)
    {
        _taskTypeRepository = taskTypeRepository;
    }

    public async Task<Result<IEnumerable<TaskTypeDto>>> Handle(
        GetTaskTypesQuery request,
        CancellationToken cancellationToken)
    {
        var taskTypes = await _taskTypeRepository.GetAllAsync(cancellationToken);

        var dtos = taskTypes.Select(tt => tt.ToDto());

        return Result.Success(dtos);
    }
}