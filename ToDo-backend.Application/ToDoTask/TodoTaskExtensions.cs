using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask;

public static class TodoTaskExtensions
{
    public static TodoTaskDto ToDto(this TodoTask todoTask)
    {
        return new TodoTaskDto(
            todoTask.Id,
            todoTask.TaskTypeId,
            todoTask.Title,
            todoTask.Description,
            todoTask.Status,
            todoTask.CreatedOnUtc,
            todoTask.LastModifiedOnUtc
        );
    }

    public static TodoTask ToEntity(this CreateTodoTaskDto dto, TaskType taskType, Guid createdBy, DateTimeOffset createdOnUtc)
    {
        return TodoTask.Create(
            createdBy,
            taskType,
            dto.Title,
            dto.Description,
            createdOnUtc.DateTime
        );
    }
}