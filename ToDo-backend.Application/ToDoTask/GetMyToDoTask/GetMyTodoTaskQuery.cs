using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.Common.Pagination;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.GetMyToDoTask;

public record GetMyTodoTaskQuery(
    int Page = 1,
    int PageSize = 10,
    int? TaskTypeId = null,
    Domain.Tasks.TaskStatus? Status = null,
    bool? SortByLastModifiedAscending = null) : IQuery<PaginatedResult<TodoTaskDto>>;