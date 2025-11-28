using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.TaskTypes;

public static class TaskTypeExtensions
{
    public static TaskTypeDto ToDto(this TaskType taskType)
    {
        return new TaskTypeDto(
            taskType.Id,
            taskType.Name,
            taskType.ColorHex
        );
    }

    public static TaskType ToEntity(this CreateTaskTypeDto dto, Guid createdBy)
    {
        return TaskType.Create(
            dto.Name,
            dto.ColorHex,
            createdBy
        );
    }
}