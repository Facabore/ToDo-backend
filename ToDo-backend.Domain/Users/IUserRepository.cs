using ToDo_backend.Domain.Abstractions;

namespace ToDo_backend.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

}
