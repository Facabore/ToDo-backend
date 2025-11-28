namespace ToDo_backend.Infrastructure.Configurations;

#region usings
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_backend.Domain.Tasks;
#endregion

public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
{
    #region Constants
    private const string TableName = "tasktypes";
    private const int MaxLengthName = 100;
    private const int MaxLengthColorHex = 7;
    #endregion

    public void Configure(EntityTypeBuilder<TaskType> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(tt => tt.Id);

        builder.Property(tt => tt.Name)
            .IsRequired()
            .HasMaxLength(MaxLengthName);

        builder.Property(tt => tt.ColorHex)
            .IsRequired()
            .HasMaxLength(MaxLengthColorHex);
        builder.HasIndex(tt => tt.CreatedBy);

        builder.HasData(new TaskType(1, "Uncategorized", "#CCCCCC", Guid.Empty));
    }

}