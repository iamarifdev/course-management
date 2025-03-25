using CourseManagement.Application.Base;

namespace CourseManagement.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler(IUserService userService)
    : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await userService.GetUserInfoAsync(x => x.Id == request.UserId, cancellationToken);
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
