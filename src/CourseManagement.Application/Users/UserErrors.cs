using CourseManagement.Application.Base;

namespace CourseManagement.Application.Users;

public static class UserErrors
{
    public static readonly Error UnableToHashPassword = Error.Failure(
        "User.UnableToHashPassword",
        "The system is unable to hash the . Contact support.");
    
    public static readonly Error UnableToVerifyPassword = Error.Failure(
        "User.UnableToVerifyPassword",
        "The system is unable to verify the provided password. Contact support.");

    public static readonly Error UserNotFound = Error.NotFound(
        "User.NotFound",
        "The user with the specified identifier was not found");

    public static readonly Error UserNotFoundByEmail = Error.NotFound(
        "User.UserNotFoundByEmail",
        "The user with the specified email was not found");

    public static readonly Error InvalidCredentials = Error.Failure(
        "User.InvalidCredentials",
        "The provided credentials are invalid");
}
