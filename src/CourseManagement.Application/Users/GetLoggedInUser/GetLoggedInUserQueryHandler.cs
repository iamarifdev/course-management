using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Users;

namespace CourseManagement.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler(IUserRepository userRepository, IUserContext userContext)
    : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userContext.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);
        }

        return new UserResponse(
            user.Id,
            user.Email.Value,
            user.Name.FirstName,
            user.Name.LastName
        );
    }
}
