using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks.Resources;

namespace ToDo_backend.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error TaskNotFound = new(
        nameof(TaskResources.TaskNotFound),
        TaskResources.TaskNotFound);

    public static readonly Error TaskTypeNotFound = new(
        nameof(TaskResources.TaskTypeNotFound),
        TaskResources.TaskTypeNotFound);

    public static readonly Error InvalidTaskStatus = new(
        nameof(TaskResources.InvalidTaskStatus),
        TaskResources.InvalidTaskStatus);

    public static Error TaskTypeNotOwnedByUser() => new(
        nameof(TaskResources.TaskTypeNotOwnedByUser),
        TaskResources.TaskTypeNotOwnedByUser);

    public static Error TaskNotOwnedByUser() => new(
        nameof(TaskResources.TaskNotOwnedByUser),
        TaskResources.TaskNotOwnedByUser);
}