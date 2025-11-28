using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;

namespace ToDo_backend.Application.TaskTypes.Get;

public record GetTaskTypeQuery(int Id) : IQuery<TaskTypeDto>;