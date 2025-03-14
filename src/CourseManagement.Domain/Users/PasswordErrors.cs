using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Users;

public class PasswordErrors
{
    public static readonly Error SaltParsingError = Error.Problem(
        "Password.SaltParsingError",
        "Password salt parsing error");
}