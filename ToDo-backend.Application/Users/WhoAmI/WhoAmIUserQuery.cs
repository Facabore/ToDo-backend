using ToDo_backend.Application.Common.Abstractions.CQRS;

namespace ToDo_backend.Application.Users.WhoAmI;

public record WhoAmIUserQuery() : IQuery<Dtos.User>;