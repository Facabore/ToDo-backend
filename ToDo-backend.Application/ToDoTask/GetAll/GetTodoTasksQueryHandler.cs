using Microsoft.EntityFrameworkCore;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.Common.Pagination;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.GetAll;

internal sealed class GetTodoTasksQueryHandler : IQueryHandler<GetTodoTasksQuery, PaginatedResult<TodoTaskDto>>
{
    private readonly ITodoTaskRepository _todoTaskRepository;

    public GetTodoTasksQueryHandler(ITodoTaskRepository todoTaskRepository)
    {
        _todoTaskRepository = todoTaskRepository;
    }

    public async Task<Result<PaginatedResult<TodoTaskDto>>> Handle(
        GetTodoTasksQuery request,
        CancellationToken cancellationToken)
    {
        var query = _todoTaskRepository.GetQueryable().AsNoTracking();

        var paginatedResult = await PaginatedResult<TodoTaskDto>.CreateAsync(
            query.Select(t => t.ToDto()),
            request.Page,
            request.PageSize);

        return Result.Success(paginatedResult);
    }
}