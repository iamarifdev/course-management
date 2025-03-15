using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    void Add(User user);
}