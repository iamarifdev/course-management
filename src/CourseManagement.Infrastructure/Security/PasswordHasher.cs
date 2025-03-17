using System.Security.Cryptography;
using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;

namespace CourseManagement.Infrastructure.Security;

/// <summary>
/// Ref: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-9.0
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public Result<string> Hash(string password)
    {
        try
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            var passwordHash = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
            return Result.Success(passwordHash);
        }
        catch (Exception)
        {
            // TODO: log the error message
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }
    }

    public Result<bool> Verify(string password, string hashedPassword)
    {
        try
        {
            var parts = hashedPassword.Split('-');
            if (parts.Length != 2)
            {
                return false;
            }

            byte[] storedHash = Convert.FromHexString(parts[0]);
            byte[] storedSalt = Convert.FromHexString(parts[1]);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, storedSalt, Iterations, Algorithm, HashSize);

            var isValid = CryptographicOperations.FixedTimeEquals(storedHash, hash);
            return isValid ? Result.Success(isValid) : Result.Failure<bool>(UserErrors.InvalidCredentials);
        }
        catch (Exception)
        {
            // TODO: log the error message
            return Result.Failure<bool>(UserErrors.InvalidCredentials);
        }
    }
}