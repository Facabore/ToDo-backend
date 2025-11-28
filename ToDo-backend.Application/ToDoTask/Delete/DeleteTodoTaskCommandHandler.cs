using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Tasks;

namespace ToDo_backend.Application.ToDoTask.Delete;

internal sealed class DeleteTodoTaskCommandHandler : ICommandHandler<DeleteTodoTaskCommand>
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoTaskCommandHandler(
        ITodoTaskRepository todoTaskRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _todoTaskRepository = todoTaskRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteTodoTaskCommand request,
        CancellationToken cancellationToken)
    {
        var todoTask = await _todoTaskRepository.GetByIdAsync(request.Id, cancellationToken);

        if (todoTask is null)
        {
            return Result.Failure(TaskErrors.TaskNotFound);
        }

        if (todoTask.CreatedBy != _userContext.UserId)
        {
            return Result.Failure(TaskErrors.TaskNotOwnedByUser());
        }

        _todoTaskRepository.Delete(todoTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}