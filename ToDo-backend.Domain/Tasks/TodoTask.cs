namespace ToDo_backend.Domain.Tasks;

#region usings
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Users;
#endregion


public class TodoTask : Entity
{
    private TodoTask(
        Guid id,
        Guid createdBy,
        int taskTypeId,
        string? title,
        string? description,
        TaskStatus status,
        DateTimeOffset createdOnUtc
    ) : base(id)
    {
        CreatedBy = createdBy;
        TaskTypeId = taskTypeId;
        Title = title;
        Description = description;
        Status = status;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid CreatedBy { get; private set; }
    public int TaskTypeId { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskType TaskType { get; private set; }
    public User? User { get; private set; }
    public DateTimeOffset CreatedOnUtc { get; private set; }
    public DateTimeOffset? LastModifiedOnUtc { get; private set; }

    public static TodoTask Create(
        Guid createdBy,
        TaskType taskType,
        string? title,
        string? description,
        DateTime createdOnUtc)
    {
        var task = new TodoTask(
            Guid.NewGuid(),
            createdBy,
            taskType.Id,
            title,
            description,
            TaskStatus.Pending,
            createdOnUtc);

        return task;
    }

    public void UpdateDetails(string? title, string? description, int taskTypeId, DateTimeOffset dateTime)
    {
        Title = title;
        Description = description;
        TaskTypeId = taskTypeId;
        LastModifiedOnUtc = dateTime;
    }

    public void MarkAsComplete(DateTimeOffset dateTime)
    {
        if (Status == TaskStatus.Completed) return;
        Status = TaskStatus.Completed;

        LastModifiedOnUtc = dateTime;
    }

    public void MarkInProgress(DateTimeOffset dateTime)
    {
        if (Status == TaskStatus.InProgress) return;
        Status = TaskStatus.InProgress;

        LastModifiedOnUtc = dateTime;
    }

    public void MarkAsPending(DateTimeOffset dateTime)
    {
        if (Status == TaskStatus.Pending) return;
        Status = TaskStatus.Pending;

        LastModifiedOnUtc = dateTime;
    }

    public void AssignDefaultId()
    {
        TaskTypeId = 1;
    }
}