namespace ToDo_backend.Application.Users.Register;

using ToDo_backend.Application.Common.Abstractions.CQRS;

public record RegisterUserCommand(string Email, string Password) : ICommand<Guid>;