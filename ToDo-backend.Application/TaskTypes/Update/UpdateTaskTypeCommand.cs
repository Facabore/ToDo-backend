using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.TaskTypes.Dtos;

namespace ToDo_backend.Application.TaskTypes.Update;

public record UpdateTaskTypeCommand(UpdateTaskTypeDto TaskType) : ICommand<TaskTypeDto>;