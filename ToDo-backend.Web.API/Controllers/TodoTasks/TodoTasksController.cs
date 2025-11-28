using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_backend.Application.ToDoTask.Create;
using ToDo_backend.Application.ToDoTask.Delete;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Application.ToDoTask.Get;
using ToDo_backend.Application.ToDoTask.GetAll;
using ToDo_backend.Application.ToDoTask.GetMyToDoTask;
using ToDo_backend.Application.ToDoTask.MarkAsComplete;
using ToDo_backend.Application.ToDoTask.MarkAsInProgress;
using ToDo_backend.Application.ToDoTask.MarkAsPending;
using ToDo_backend.Application.ToDoTask.Update;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Web.API.Controllers.TodoTasks;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V)]
[Route("api/todotasks")]
public class TodoTasksController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetTodoTasks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var query = new GetTodoTasksQuery(page, pageSize);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTodoTasks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? taskTypeId = null,
        [FromQuery] string? status = null,
        [FromQuery] bool? sortByLastModifiedAscending = null,
        CancellationToken cancellationToken = default)
    {
        Domain.Tasks.TaskStatus? taskStatus = null;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<Domain.Tasks.TaskStatus>(status, true, out var parsedStatus))
        {
            taskStatus = parsedStatus;
        }

        var query = new GetMyTodoTaskQuery(page, pageSize, taskTypeId, taskStatus, sortByLastModifiedAscending);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTodoTask(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTodoTaskQuery(id);
        Result<TodoTaskDto> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoTask(CreateTodoTaskDto request, CancellationToken cancellationToken)
    {
        var command = new CreateTodoTaskCommand(request);
        Result<TodoTaskDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created("", result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTodoTask(Guid id, UpdateTodoTaskDto request, CancellationToken cancellationToken)
    {
        var command = new UpdateTodoTaskCommand(id, request);
        Result<TodoTaskDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> MarkTodoTaskAsComplete(Guid id, CancellationToken cancellationToken)
    {
        var command = new MarkTodoTaskAsCompleteCommand(id);
        Result<TodoTaskDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}/inprogress")]
    public async Task<IActionResult> MarkTodoTaskAsInProgress(Guid id, CancellationToken cancellationToken)
    {
        var command = new MarkTodoTaskAsInProgressCommand(id);
        Result<TodoTaskDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id:guid}/pending")]
    public async Task<IActionResult> MarkTodoTaskAsPending(Guid id, CancellationToken cancellationToken)
    {
        var command = new MarkTodoTaskAsPendingCommand(id);
        Result<TodoTaskDto> result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTodoTask(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteTodoTaskCommand(id);
        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}