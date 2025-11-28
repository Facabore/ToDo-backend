using ToDo_backend.Domain.Abstractions;

namespace ToDo_backend.Domain.Tasks;

public interface ITaskTypeRepository : IRepository<TaskType>
{
    Task<TaskType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskType>> GetByCreatedByAsync(Guid createdBy, CancellationToken cancellationToken = default);
}