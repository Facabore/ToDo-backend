using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Application.Users.Dtos;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Users;
using User = ToDo_backend.Application.Users.Dtos.User;

namespace ToDo_backend.Application.Users.WhoAmI;

internal sealed class WhoAmIUserHandler : IQueryHandler<WhoAmIUserQuery, Dtos.User>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public WhoAmIUserHandler(
        IUserRepository userRepository,
        IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<Result<Dtos.User>> Handle(WhoAmIUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(_userContext.UserId, cancellationToken);

        if (user is null) return Result.Failure<Dtos.User>(UserErrors.NotFound);

        var response = new Dtos.User(
            user.Id,
            user.Email,
            user.CreatedAt
        );

        return response;
    }
}