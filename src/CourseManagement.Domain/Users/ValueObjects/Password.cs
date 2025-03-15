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
            return Result.Failure<Password>(UserErrors.UnableToParsePasswordSalt);
        }
    }

    public static Result<bool> VerifyHash(string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (SaltParseException)
        {
            return Result.Failure<bool>(UserErrors.InvalidCredentials);
        }
    }
}