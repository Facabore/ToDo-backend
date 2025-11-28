namespace ToDo_backend.Application.ToDoTask.Dtos;

public record CreateTodoTaskDto(
    int TaskTypeId,
    string? Title,
    string? Description
);