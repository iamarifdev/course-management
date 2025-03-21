using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;

namespace CourseManagement.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler(IUserService userService, IUserContext userContext)
    : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userService.GetUserInfoAsync(x => x.Id == userContext.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);
        }

        return new UserResponse(
            user.Id,
            user.UserId,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Department
        );
    }
}
