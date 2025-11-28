namespace ToDo_backend.Infrastructure.Repositories;

#region Usings
using Microsoft.EntityFrameworkCore;
using ToDo_backend.Domain.Users;
using ToDo_backend.Infrastructure.Database; 
#endregion

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }


}