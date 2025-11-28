using FluentAssertions;
using Moq;
using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.ToDoTask.Create;
using ToDo_backend.Application.ToDoTask.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;
using Xunit;

namespace ToDo_backend.Tests.ToDoTask.Create;

public class CreateTodoTaskCommandHandlerTests
{
    private readonly Mock<ITodoTaskRepository> _todoTaskRepositoryMock;
    private readonly Mock<ITaskTypeRepository> _taskTypeRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly CreateTodoTaskCommandHandler _handler;

    public CreateTodoTaskCommandHandlerTests()
    {
        _todoTaskRepositoryMock = new Mock<ITodoTaskRepository>();
        _taskTypeRepositoryMock = new Mock<ITaskTypeRepository>();
        _userContextMock = new Mock<IUserContext>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();

        _handler = new CreateTodoTaskCommandHandler(
            _todoTaskRepositoryMock.Object,
            _taskTypeRepositoryMock.Object,
            _userContextMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateTodoTaskSuccessfully_WhenTaskTypeExists()
    {
        // Arrange
        var taskTypeId = 1;
        var userId = Guid.NewGuid();
        var utcNow = DateTime.UtcNow;
        var taskType = new TaskType(taskTypeId, "Test Type", "#FFFFFF", userId);
        var createDto = new CreateTodoTaskDto(taskTypeId, "Test Title", "Test Description");
        var command = new CreateTodoTaskCommand(createDto);

        _taskTypeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(taskTypeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskType);

        _userContextMock.Setup(ctx => ctx.UserId).Returns(userId);
        _dateTimeProviderMock.Setup(provider => provider.UtcNow).Returns(utcNow);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.TaskTypeId.Should().Be(taskTypeId);
        result.Value.Title.Should().Be(createDto.Title);
        result.Value.Description.Should().Be(createDto.Description);
        result.Value.Status.Should().Be(ToDo_backend.Domain.Tasks.TaskStatus.Pending);

        _todoTaskRepositoryMock.Verify(repo => repo.Add(It.IsAny<TodoTask>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenTaskTypeDoesNotExist()
    {
        // Arrange
        var taskTypeId = 1;
        var createDto = new CreateTodoTaskDto(taskTypeId, "Test Title", "Test Description");
        var command = new CreateTodoTaskCommand(createDto);

        _taskTypeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(taskTypeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskType?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(TaskErrors.TaskTypeNotFound);

        _todoTaskRepositoryMock.Verify(repo => repo.Add(It.IsAny<TodoTask>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenSaveChangesFails()
    {
        // Arrange
        var taskTypeId = 1;
        var userId = Guid.NewGuid();
        var utcNow = DateTime.UtcNow;
        var taskType = new TaskType(taskTypeId, "Test Type", "#FFFFFF", userId);
        var createDto = new CreateTodoTaskDto(taskTypeId, "Test Title", "Test Description");
        var command = new CreateTodoTaskCommand(createDto);

        _taskTypeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(taskTypeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskType);

        _userContextMock.Setup(ctx => ctx.UserId).Returns(userId);
        _dateTimeProviderMock.Setup(provider => provider.UtcNow).Returns(utcNow);

        _unitOfWorkMock
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

        _todoTaskRepositoryMock.Verify(repo => repo.Add(It.IsAny<TodoTask>()), Times.Once);
    }
}