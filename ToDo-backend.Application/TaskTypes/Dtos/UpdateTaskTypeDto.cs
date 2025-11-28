namespace ToDo_backend.Application.TaskTypes.Dtos;

public record UpdateTaskTypeDto(
    int Id,
    string Name,
    string ColorHex
);