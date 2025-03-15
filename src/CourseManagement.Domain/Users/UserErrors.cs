using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Users;

public class UserErrors
{
    public static readonly Error UnableToParsePasswordSalt = Error.Problem(
        "User.UnableToParsePasswordSalt",
        "The password salt cannot be parsed.");

    public static readonly Error UserNotFound = Error.NotFound(
        "User.NotFound",
        "The user with the specified identifier was not found");

    public static readonly Error UserNotFoundByEmail = Error.NotFound(
        "User.UserNotFoundByEmail",
        "The user with the specified email was not found");

    public static readonly Error InvalidCredentials = Error.NotFound(
        "User.InvalidCredentials",
        "The provided credentials are invalid");
}