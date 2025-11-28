using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;

namespace ToDo_backend.Application.TaskTypes.GetAll;

public record GetTaskTypesQuery : IQuery<IEnumerable<TaskTypeDto>>;