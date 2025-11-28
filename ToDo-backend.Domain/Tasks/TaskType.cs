namespace ToDo_backend.Domain.Tasks;

using ToDo_backend.Domain.Abstractions;

public sealed class TaskType : Entity
{
    public TaskType(int id,
        string name,
        string colorHex,
        Guid createdBy)
    {
        Id = id;
        Name = name;
        ColorHex = colorHex;
        CreatedBy = createdBy;
    }

    private TaskType(){}

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string ColorHex { get; private set; }
    public Guid CreatedBy { get; private set; }

    public static TaskType Create(
        string name,
        string colorHex,
        Guid createdBy)
    {
        var taskType = new TaskType(0, name, colorHex, createdBy);
        return taskType;
    }

    public void Update(
        string name,
        string colorHex)
    {
        Name = name;
        ColorHex = colorHex;
    }

}