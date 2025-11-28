using ToDo_backend.Application.Common.Abstractions.CQRS;

namespace ToDo_backend.Application.ToDoTask.Delete;

public record DeleteTodoTaskCommand(Guid Id) : ICommand;