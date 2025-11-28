namespace ToDo_backend.Infrastructure.Configurations;

#region usings
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo_backend.Domain.Users;
#endregion

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const string TableName = "users";
    private const int MaxEmailLength = 255;
    private const int MaxPasswordLength = 255;
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(MaxEmailLength);

        builder.Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(MaxPasswordLength);

        builder.HasIndex(user => user.Email).IsUnique();
    }
}