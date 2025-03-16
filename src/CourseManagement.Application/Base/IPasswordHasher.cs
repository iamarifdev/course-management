using CourseManagement.Domain.Base;

namespace CourseManagement.Application.Base;

public interface IPasswordHasher
{
    Result<string> Hash(string password);
    Result<bool> Verify(string password, string hashedPassword);
}
