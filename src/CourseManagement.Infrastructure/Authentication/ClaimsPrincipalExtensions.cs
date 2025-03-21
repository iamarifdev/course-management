using System.Security.Claims;

namespace CourseManagement.Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal? principal)
    {
        var id = principal?.FindFirstValue(ClaimTypes.Sid);

        return Guid.TryParse(id, out var parsedUserId)
            ? parsedUserId
            : throw new ApplicationException("Id is unavailable");
    }

    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out var parsedUserId)
            ? parsedUserId
            : throw new ApplicationException("User id is unavailable");
    }

    public static string GetUserEmail(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.Email) ??
               throw new ApplicationException("User email is unavailable");
    }

    public static string GetUserRole(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.Role) ??
               throw new ApplicationException("User role is unavailable");
    }
}
