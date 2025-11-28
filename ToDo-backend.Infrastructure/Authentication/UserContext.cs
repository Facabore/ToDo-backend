namespace ToDo_backend.Infrastructure.Authentication;

#region usings
using Microsoft.AspNetCore.Http;
using ToDo_backend.Application.Common.Abstractions.Authentication;
#endregion

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");
}