namespace ToDo_backend.Domain.Users;

#region usings
using ToDo_backend.Domain.Abstractions;
#endregion

public sealed class User : Entity
{
    private User(
        Guid id,
        string email,
        string passwordHash,
        DateTime createdAt)
        : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static User Register(
        string email,
        string passwordHash,
        DateTime createdAt)
    {
        var user = new User(
            Guid.NewGuid(),
            email,
            passwordHash,
            createdAt);

        return user;
    }

}
