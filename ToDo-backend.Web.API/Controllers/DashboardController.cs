using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_backend.Application.Common.Responses;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Application.ToDoTask.GetDashboardStats;
using ToDo_backend.Domain.Abstractions;

namespace ToDo_backend.Web.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V)]
[Route("api/dashboard")]
public class DashboardController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("stats")]
    public async Task<IActionResult> GetDashboardStats(CancellationToken cancellationToken)
    {
        var query = new GetDashboardStatsQuery();
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(ApiResponse<DashboardStatsDto>.FromData(result.Value))
            : BadRequest(result.Error);
    }
}