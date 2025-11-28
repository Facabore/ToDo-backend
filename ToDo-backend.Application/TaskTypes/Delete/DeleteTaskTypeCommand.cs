using ToDo_backend.Application.Common.Abstractions.CQRS;

namespace ToDo_backend.Application.TaskTypes.Delete;

public record DeleteTaskTypeCommand(int Id) : ICommand;