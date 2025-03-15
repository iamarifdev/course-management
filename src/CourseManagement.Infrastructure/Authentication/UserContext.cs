using CourseManagement.Application.Base.Authentication;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                          throw new ApplicationException("User context is unavailable");

    public string Email => httpContextAccessor.HttpContext?.User.GetUserEmail() ??
                           throw new ApplicationException("User context is unavailable");

    public string Role => httpContextAccessor.HttpContext?.User.GetUserRole() ??
                          throw new ApplicationException("User context is unavailable");
}
