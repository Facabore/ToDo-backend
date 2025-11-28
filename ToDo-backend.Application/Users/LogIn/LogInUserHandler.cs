namespace ToDo_backend.Application.Users.LogIn;

#region usings
using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Users;
#endregion

internal sealed class LogInUserHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashingService _hashingService;

    public LogInUserHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IHashingService hashingService)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hashingService = hashingService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null) return Result.Failure<AccessTokenResponse>(UserErrors.NotFound);

        if(!_hashingService.VerifyPassword(user.PasswordHash, request.Password)) return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);

        var token = _jwtProvider.GenerateToken(user);

        return Result.Success(new AccessTokenResponse(token));
    }
}