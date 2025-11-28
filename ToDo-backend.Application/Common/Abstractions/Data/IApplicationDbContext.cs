using Microsoft.EntityFrameworkCore;
using ToDo_backend.Domain.Tasks;
using ToDo_backend.Domain.Users;

namespace ToDo_backend.Application.Common.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoTask> TodoTasks { get; }
    DbSet<TaskType> TaskTypes { get; }
}