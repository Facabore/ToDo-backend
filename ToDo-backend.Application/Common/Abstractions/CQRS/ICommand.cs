namespace ToDo_backend.Application.Common.Abstractions.CQRS;

using MediatR;
using ToDo_backend.Domain.Abstractions;

public interface IBaseCommand
{

}

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}