using System.Security.Claims;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.Base.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, Email email, Role role);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
    ClaimsPrincipal GetClaimsPrincipal(string token);
}
