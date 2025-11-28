using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.Update;

public record UpdateTodoTaskCommand(Guid Id, UpdateTodoTaskDto TodoTask) : ICommand<TodoTaskDto>;