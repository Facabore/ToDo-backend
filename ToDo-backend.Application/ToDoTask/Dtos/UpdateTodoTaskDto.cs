namespace ToDo_backend.Application.ToDoTask.Dtos;

public record UpdateTodoTaskDto(
    string? Title,
    string? Description,
    int TaskTypeId
);