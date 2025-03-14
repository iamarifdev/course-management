using BCrypt.Net;
using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Users.ValueObjects;

public record Password(string Value)
{
    public static Result<Password> Hash(string password)
    {
        try
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return new Password(hash);
        }
        catch (SaltParseException)
        {
            // TODO: log it
            return Result.Failure<Password>(PasswordErrors.SaltParsingError);
        }
    }
}