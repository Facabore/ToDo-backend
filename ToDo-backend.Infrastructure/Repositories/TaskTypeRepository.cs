using Microsoft.EntityFrameworkCore;
using ToDo_backend.Domain.Tasks;
using ToDo_backend.Infrastructure.Database;

namespace ToDo_backend.Infrastructure.Repositories;

internal sealed class TaskTypeRepository : Repository<TaskType>, ITaskTypeRepository
{
    public TaskTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TaskType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TaskType>().FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<TaskType>> GetByCreatedByAsync(Guid createdBy, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TaskType>()
            .Where(tt => tt.CreatedBy == createdBy || tt.CreatedBy == Guid.Empty)
            .ToListAsync(cancellationToken);
    }
}