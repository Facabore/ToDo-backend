namespace ToDo_backend.Infrastructure.Authorization;

#region Usings
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using ToDo_backend.Infrastructure.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
#endregion

internal sealed class CustomClaimsTransformation(IServiceProvider serviceProvider) : IClaimsTransformation
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(claim => claim.Type is JwtRegisteredClaimNames.Sub))
        {
            return Task.FromResult(principal);
        }

        using var scope = _serviceProvider.CreateScope();
        var identityId = principal.GetUserId();
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, identityId.ToString()));
        principal.AddIdentity(claimsIdentity);

        return Task.FromResult(principal);
    }
}