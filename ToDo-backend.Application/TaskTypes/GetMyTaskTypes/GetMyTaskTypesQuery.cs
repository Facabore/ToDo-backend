using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;

namespace ToDo_backend.Application.TaskTypes.GetMyTaskTypes;

public record GetMyTaskTypesQuery : IQuery<IEnumerable<TaskTypeDto>>;