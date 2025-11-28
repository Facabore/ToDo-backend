using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.Common.Pagination;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.GetAll;

public record GetTodoTasksQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedResult<TodoTaskDto>>;