using Microsoft.EntityFrameworkCore;
using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.Common.Pagination;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.GetMyToDoTask;

internal sealed class GetMyTodoTaskQueryHandler : IQueryHandler<GetMyTodoTaskQuery, PaginatedResult<TodoTaskDto>>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUserContext _userContext;

    public GetMyTodoTaskQueryHandler(ITodoTaskRepository todoTaskRepository, IUserContext userContext)
    {
        _todoTaskRepository = todoTaskRepository;
        _userContext = userContext;
    }

    public async Task<Result<PaginatedResult<TodoTaskDto>>> Handle(
        GetMyTodoTaskQuery request,
        CancellationToken cancellationToken)
    {
        var query = _todoTaskRepository.GetQueryable()
            .Where(t => t.CreatedBy == _userContext.UserId)
            .AsNoTracking();

        if (request.TaskTypeId.HasValue)
        {
            query = query.Where(t => t.TaskTypeId == request.TaskTypeId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(t => t.Status == request.Status.Value);
        }

        if (request.SortByLastModifiedAscending.HasValue)
        {
            query = request.SortByLastModifiedAscending.Value
                ? query.OrderBy(t => t.LastModifiedOnUtc ?? t.CreatedOnUtc)
                : query.OrderByDescending(t => t.LastModifiedOnUtc ?? t.CreatedOnUtc);
        }

        var paginatedResult = await PaginatedResult<TodoTaskDto>.CreateAsync(
            query.Select(t => t.ToDto()),
            request.Page,
            request.PageSize);

        return Result.Success(paginatedResult);
    }
}