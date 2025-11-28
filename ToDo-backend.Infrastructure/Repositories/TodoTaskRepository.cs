using Microsoft.EntityFrameworkCore;
using ToDo_backend.Domain.Tasks;
using ToDo_backend.Infrastructure.Database;
using TaskStatus = ToDo_backend.Domain.Tasks.TaskStatus;

namespace ToDo_backend.Infrastructure.Repositories;

internal sealed class TodoTaskRepository : Repository<TodoTask>, ITodoTaskRepository
{
    public TodoTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> CountTotalTasksAsync(Guid userId)
    {
        return await _dbContext.Set<TodoTask>().CountAsync(x => x.CreatedBy == userId);
    }

    public async Task<int> CountCompletedTasksAsync(Guid userId)
    {
        return await _dbContext.Set<TodoTask>().CountAsync(x => x.CreatedBy == userId && x.Status == TaskStatus.Completed);
    }

    public async Task<int> CountInProgressTasksAsync(Guid userId)
    {
        return await _dbContext.Set<TodoTask>().CountAsync(x => x.CreatedBy == userId && x.Status == TaskStatus.InProgress);
    }

    public async Task<int> CountPendingTasksAsync(Guid userId)
    {
        return await _dbContext.Set<TodoTask>().CountAsync(x => x.CreatedBy == userId && x.Status == TaskStatus.Pending);
    }
}