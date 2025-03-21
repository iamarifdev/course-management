using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
}
