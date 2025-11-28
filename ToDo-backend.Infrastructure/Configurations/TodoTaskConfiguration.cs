namespace ToDo_backend.Infrastructure.Configurations;

#region usings
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_backend.Domain.Tasks;
#endregion

public class TodoTaskConfiguration : IEntityTypeConfiguration<TodoTask>
{
    #region Constants
    private const string TableName = "todotasks";
    private const int MaxLengthTitle = 200;
    private const int MaxLengthDescription = 1000;
    #endregion

    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder.ToTable(TableName);

        builder.Property(t => t.Title)
            .HasMaxLength(MaxLengthTitle);

        builder.Property(t => t.Description)
            .HasMaxLength(MaxLengthDescription);

        builder.HasOne(t => t.TaskType)
            .WithMany()
            .HasForeignKey(t => t.TaskTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.CreatedBy);
    }
}