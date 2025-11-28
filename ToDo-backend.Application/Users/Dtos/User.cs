namespace ToDo_backend.Application.Users.Dtos;

public record User(
    Guid Id,
    string Email,
    DateTime CreatedAt
);
