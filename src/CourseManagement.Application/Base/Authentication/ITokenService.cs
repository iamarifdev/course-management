namespace CourseManagement.Application.Base.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(Guid id, Guid userId, string email, string role);
    string GenerateRefreshToken();
}
