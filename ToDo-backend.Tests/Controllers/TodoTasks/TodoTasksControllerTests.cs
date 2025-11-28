using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDo_backend.Application.Common.Pagination;
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
using ToDo_backend.Web.API.Controllers.TodoTasks;
using Xunit;

namespace ToDo_backend.Tests.Controllers.TodoTasks;

public class TodoTasksControllerTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly TodoTasksController _controller;

    public TodoTasksControllerTests()
    {
        _senderMock = new Mock<ISender>();
        _controller = new TodoTasksController(_senderMock.Object);
    }

    [Fact]
    public async Task GetTodoTasks_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var expectedResult = PaginatedResult<TodoTaskDto>.Create(new List<TodoTaskDto>(), 1, 10, 0);
        _senderMock.Setup(s => s.Send(It.IsAny<GetTodoTasksQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedResult));

        // Act
        var result = await _controller.GetTodoTasks(1, 10, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedResult);
        _senderMock.Verify(s => s.Send(It.IsAny<GetTodoTasksQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTodoTasks_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<GetTodoTasksQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<PaginatedResult<TodoTaskDto>>(error));

        // Act
        var result = await _controller.GetTodoTasks(1, 10, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task GetMyTodoTasks_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var expectedResult = PaginatedResult<TodoTaskDto>.Create(new List<TodoTaskDto>(), 1, 10, 0);
        _senderMock.Setup(s => s.Send(It.IsAny<GetMyTodoTaskQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedResult));

        // Act
        var result = await _controller.GetMyTodoTasks(1, 10, null, null, null, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedResult);
        _senderMock.Verify(s => s.Send(It.IsAny<GetMyTodoTaskQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetMyTodoTasks_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<GetMyTodoTaskQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<PaginatedResult<TodoTaskDto>>(error));

        // Act
        var result = await _controller.GetMyTodoTasks(1, 10, null, null, null, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task GetTodoTask_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedDto = new TodoTaskDto(id, 1, "Title", "Description", Domain.Tasks.TaskStatus.Pending, DateTime.UtcNow, DateTime.UtcNow);
        _senderMock.Setup(s => s.Send(It.IsAny<GetTodoTaskQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedDto));

        // Act
        var result = await _controller.GetTodoTask(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedDto);
        _senderMock.Verify(s => s.Send(It.Is<GetTodoTaskQuery>(q => q.Id == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetTodoTask_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<GetTodoTaskQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<TodoTaskDto>(error));

        // Act
        var result = await _controller.GetTodoTask(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task CreateTodoTask_ShouldReturnCreated_WhenSuccessful()
    {
        // Arrange
        var createDto = new CreateTodoTaskDto(1, "Title", "Description");
        var expectedDto = new TodoTaskDto(Guid.NewGuid(), 1, "Title", "Description", Domain.Tasks.TaskStatus.Pending, DateTime.UtcNow, DateTime.UtcNow);
        _senderMock.Setup(s => s.Send(It.IsAny<CreateTodoTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedDto));

        // Act
        var result = await _controller.CreateTodoTask(createDto, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreatedResult>();
        var createdResult = result as CreatedResult;
        createdResult!.Value.Should().Be(expectedDto);
        _senderMock.Verify(s => s.Send(It.Is<CreateTodoTaskCommand>(c => c.TodoTask == createDto), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateTodoTask_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var createDto = new CreateTodoTaskDto(1, "Title", "Description");
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<CreateTodoTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<TodoTaskDto>(error));

        // Act
        var result = await _controller.CreateTodoTask(createDto, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task UpdateTodoTask_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateDto = new UpdateTodoTaskDto("New Title", "New Description", 1);
        var expectedDto = new TodoTaskDto(id, 1, "New Title", "New Description", Domain.Tasks.TaskStatus.Pending, DateTime.UtcNow, DateTime.UtcNow);
        _senderMock.Setup(s => s.Send(It.IsAny<UpdateTodoTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedDto));

        // Act
        var result = await _controller.UpdateTodoTask(id, updateDto, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedDto);
        _senderMock.Verify(s => s.Send(It.Is<UpdateTodoTaskCommand>(c => c.Id == id && c.TodoTask == updateDto), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTodoTask_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateDto = new UpdateTodoTaskDto("New Title", "New Description", 1);
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<UpdateTodoTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<TodoTaskDto>(error));

        // Act
        var result = await _controller.UpdateTodoTask(id, updateDto, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task MarkTodoTaskAsComplete_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedDto = new TodoTaskDto(id, 1, "Title", "Description", Domain.Tasks.TaskStatus.Completed, DateTime.UtcNow, DateTime.UtcNow);
        _senderMock.Setup(s => s.Send(It.IsAny<MarkTodoTaskAsCompleteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedDto));

        // Act
        var result = await _controller.MarkTodoTaskAsComplete(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedDto);
        _senderMock.Verify(s => s.Send(It.Is<MarkTodoTaskAsCompleteCommand>(c => c.Id == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task MarkTodoTaskAsComplete_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<MarkTodoTaskAsCompleteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<TodoTaskDto>(error));

        // Act
        var result = await _controller.MarkTodoTaskAsComplete(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task MarkTodoTaskAsInProgress_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedDto = new TodoTaskDto(id, 1, "Title", "Description", Domain.Tasks.TaskStatus.InProgress, DateTime.UtcNow, DateTime.UtcNow);
        _senderMock.Setup(s => s.Send(It.IsAny<MarkTodoTaskAsInProgressCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedDto));

        // Act
        var result = await _controller.MarkTodoTaskAsInProgress(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedDto);
        _senderMock.Verify(s => s.Send(It.Is<MarkTodoTaskAsInProgressCommand>(c => c.Id == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task MarkTodoTaskAsInProgress_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<MarkTodoTaskAsInProgressCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<TodoTaskDto>(error));

        // Act
        var result = await _controller.MarkTodoTaskAsInProgress(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task MarkTodoTaskAsPending_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedDto = new TodoTaskDto(id, 1, "Title", "Description", Domain.Tasks.TaskStatus.Pending, DateTime.UtcNow, DateTime.UtcNow);
        _senderMock.Setup(s => s.Send(It.IsAny<MarkTodoTaskAsPendingCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(expectedDto));

        // Act
        var result = await _controller.MarkTodoTaskAsPending(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(expectedDto);
        _senderMock.Verify(s => s.Send(It.Is<MarkTodoTaskAsPendingCommand>(c => c.Id == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task MarkTodoTaskAsPending_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<MarkTodoTaskAsPendingCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<TodoTaskDto>(error));

        // Act
        var result = await _controller.MarkTodoTaskAsPending(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }

    [Fact]
    public async Task DeleteTodoTask_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var id = Guid.NewGuid();
        _senderMock.Setup(s => s.Send(It.IsAny<DeleteTodoTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _controller.DeleteTodoTask(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        _senderMock.Verify(s => s.Send(It.Is<DeleteTodoTaskCommand>(c => c.Id == id), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTodoTask_ShouldReturnBadRequest_WhenFailed()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = new Error("TestError", "Test message");
        _senderMock.Setup(s => s.Send(It.IsAny<DeleteTodoTaskCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(error));

        // Act
        var result = await _controller.DeleteTodoTask(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badResult = result as BadRequestObjectResult;
        badResult!.Value.Should().Be(error);
    }
}