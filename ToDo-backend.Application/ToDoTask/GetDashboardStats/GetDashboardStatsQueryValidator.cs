using FluentValidation;

namespace ToDo_backend.Application.ToDoTask.GetDashboardStats;

public class GetDashboardStatsQueryValidator : AbstractValidator<GetDashboardStatsQuery>
{
    public GetDashboardStatsQueryValidator()
    {
        // no rules
    }
}