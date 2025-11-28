namespace ToDo_backend.Application.Common.Abstractions.CQRS;

using MediatR;
using ToDo_backend.Domain.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}