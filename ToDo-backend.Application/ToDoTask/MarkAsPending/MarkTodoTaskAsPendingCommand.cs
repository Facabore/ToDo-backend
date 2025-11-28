using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.MarkAsPending;

public record MarkTodoTaskAsPendingCommand(Guid Id) : ICommand<TodoTaskDto>;