namespace ToDo_backend.Application.Common.Abstractions.CQRS;

using MediatR;
using ToDo_backend.Domain.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{

}