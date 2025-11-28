using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_backend.Application.TaskTypes.Create;
using ToDo_backend.Application.TaskTypes.Delete;
using ToDo_backend.Application.TaskTypes.Dtos;
using ToDo_backend.Application.TaskTypes.Get;
using ToDo_backend.Application.TaskTypes.GetAll;
using ToDo_backend.Application.TaskTypes.GetMyTaskTypes;
using ToDo_backend.Application.TaskTypes.Update;
using ToDo_backend.Domain.Abstractions;

namespace ToDo_backend.Web.API.Controllers.TaskTypes;

[Authorize]
[ApiController]
[Route("api/tasktypes")]
public class TaskTypesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetTaskTypes(CancellationToken cancellationToken)
    {
        var query = new GetTaskTypesQuery();
        Result<IEnumerable<TaskTypeDto>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTaskTypes(CancellationToken cancellationToken)
    {
        var query = new GetMyTaskTypesQuery();
        Result<IEnumerable<TaskTypeDto>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTaskType(int id, CancellationToken cancellationToken)
    {
        var query = new GetTaskTypeQuery(id);
        Result<TaskTypeDto> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskType(CreateTaskTypeDto request, CancellationToken cancellationToken)
    {
        var command = new CreateTaskTypeCommand(request);
        Result<TaskTypeDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(GetTaskType), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTaskType(int id, UpdateTaskTypeDto request, CancellationToken cancellationToken)
    {
        var updatedRequest = request with { Id = id };
        var command = new UpdateTaskTypeCommand(updatedRequest);
        Result<TaskTypeDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTaskType(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteTaskTypeCommand(id);
        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}