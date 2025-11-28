using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.MarkAsComplete;

public record MarkTodoTaskAsCompleteCommand(Guid Id) : ICommand<TodoTaskDto>;