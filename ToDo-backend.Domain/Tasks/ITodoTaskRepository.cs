using ToDo_backend.Domain.Abstractions;

namespace ToDo_backend.Domain.Tasks;

public interface ITodoTaskRepository : IRepository<TodoTask>
{
    Task<int> CountTotalTasksAsync(Guid userId);
    Task<int> CountCompletedTasksAsync(Guid userId);
    Task<int> CountInProgressTasksAsync(Guid userId);
    Task<int> CountPendingTasksAsync(Guid userId);
}