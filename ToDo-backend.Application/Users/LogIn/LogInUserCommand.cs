namespace ToDo_backend.Application.Users.LogIn;

using ToDo_backend.Application.Common.Abstractions.CQRS;


public record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;