using CourseManagement.Application.Base;

namespace CourseManagement.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;