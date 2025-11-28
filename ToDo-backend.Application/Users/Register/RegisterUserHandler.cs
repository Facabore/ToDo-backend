namespace ToDo_backend.Application.Users.Register;

#region usings
using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.Common.Abstractions.CQRS;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Users;
#endregion

internal sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHashingService _hashingService;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IHashingService hashingService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _hashingService = hashingService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser is not null) return Result.Failure<Guid>(UserErrors.EmailAlreadyExists(request.Email));

        var hashedPassword = _hashingService.HashPassword(request.Password);

        var user = User.Register(
            request.Email,
            hashedPassword,
            _dateTimeProvider.UtcNow
        );

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}