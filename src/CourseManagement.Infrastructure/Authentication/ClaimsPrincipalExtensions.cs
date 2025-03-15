using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CourseManagement.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(userId, out var parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }

    public static string GetUserEmail(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(JwtRegisteredClaimNames.Email) ??
               throw new ApplicationException("User email is unavailable");
    }

    public static string GetUserRole(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.Role) ??
               throw new ApplicationException("User role is unavailable");
    }
}
