using System.Security.Claims;
using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public Guid StaffId
    {
        get
        {
            var id = User?.GetId();
            return id is not null && Role == Roles.Staff
                ? (Guid)id
                : throw new ApplicationException("User context is unavailable");
        }
    }
    
    public Guid StudentId
    {
        get
        {
            var id = User?.GetId();
            return id is not null && Role == Roles.Student
                ? (Guid)id
                : throw new ApplicationException("User context is unavailable");
        }
    }

    public Guid UserId => User?.GetUserId() ??
                          throw new ApplicationException("User context is unavailable");

    public string Email => User?.GetUserEmail() ??
                           throw new ApplicationException("User context is unavailable");

    public string Role => User?.GetUserRole() ??
                          throw new ApplicationException("User context is unavailable");
}
