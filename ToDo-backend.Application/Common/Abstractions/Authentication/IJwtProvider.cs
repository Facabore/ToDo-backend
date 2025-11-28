using ToDo_backend.Domain.Users;

namespace ToDo_backend.Application.Common.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateToken(User user);
}