using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.MarkAsInProgress;

public record MarkTodoTaskAsInProgressCommand(Guid Id) : ICommand<TodoTaskDto>;