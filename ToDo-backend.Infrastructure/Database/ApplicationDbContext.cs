namespace ToDo_backend.Infrastructure.Database;

#region Usings
using Microsoft.EntityFrameworkCore;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.Common.Abstractions.Data;
using ToDo_backend.Application.Common.Exceptions;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;
using ToDo_backend.Domain.Users;
#endregion

public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public DbSet<User> Users { get; set; }
    public DbSet<TodoTask> TodoTasks { get; }
    public DbSet<TaskType> TaskTypes { get; }

    public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }


    // We override the OnModelCreating method to apply the entity configurations.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDbFunction(() => Extensions.DbFunctionsExtensions.RemoveDiacritics(default))
            .HasName("remove_diacritics");

        base.OnModelCreating(modelBuilder);
    }

}