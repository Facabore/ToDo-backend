using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.ToDoTask.Dtos;

namespace ToDo_backend.Application.ToDoTask.GetDashboardStats;

public record GetDashboardStatsQuery() : IQuery<DashboardStatsDto>;