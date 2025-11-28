namespace ToDo_backend.Application.ToDoTask.Dtos;

public record TodoTaskDto(
    Guid Id,
    int TaskTypeId,
    string? Title,
    string? Description,
    ToDo_backend.Domain.Tasks.TaskStatus Status,
    DateTimeOffset CreatedOnUtc,
    DateTimeOffset? LastModifiedOnUtc
);