using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Users.Resources;

namespace ToDo_backend.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        nameof(UserResources.UserNotFound),
        UserResources.UserNotFound);

    public static readonly Error InvalidCredentials = new(
        nameof(UserResources.InvalidCredentials),
        UserResources.InvalidCredentials);

    public static Error EmailAlreadyExists(string email) => new(
        nameof(UserResources.EmailAlreadyExist),
        string.Format(UserResources.EmailAlreadyExist, email));
}
