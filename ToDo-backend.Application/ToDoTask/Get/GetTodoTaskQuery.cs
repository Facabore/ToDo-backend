using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.Get;

public record GetTodoTaskQuery(Guid Id) : IQuery<TodoTaskDto>;