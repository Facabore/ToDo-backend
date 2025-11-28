namespace ToDo_backend.Application.ToDoTask.GetDashboardStats;

#region usings
using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;
#endregion

internal sealed class GetDashboardStatsQueryHandler : IQueryHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUserContext _userContext;

    public GetDashboardStatsQueryHandler(
        ITodoTaskRepository todoTaskRepository,
        IUserContext userContext)
    {
        _todoTaskRepository = todoTaskRepository;
        _userContext = userContext;
    }

    public async Task<Result<DashboardStatsDto>> Handle(
        GetDashboardStatsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var totalTasks = await _todoTaskRepository.CountTotalTasksAsync(userId);
        var completedTasks = await _todoTaskRepository.CountCompletedTasksAsync(userId);
        var onProgressTasks = await _todoTaskRepository.CountInProgressTasksAsync(userId);
        var pendingTasks = await _todoTaskRepository.CountPendingTasksAsync(userId);

        var dto = new DashboardStatsDto(totalTasks, completedTasks, onProgressTasks, pendingTasks);

        return Result.Success(dto);
    }
}