namespace ToDo_backend.Application.ToDoTask.Dtos;

public record DashboardStatsDto(
    int TotalTasks,
    int CompletedTasks,
    int OnProgressTasks,
    int PendingTasks
);