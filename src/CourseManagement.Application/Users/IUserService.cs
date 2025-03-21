using System.Linq.Expressions;
using CourseManagement.Domain.Users;

namespace CourseManagement.Application.Users;

public interface IUserService
{
    Task<UserInfo?> GetUserInfoAsync(Expression<Func<User, bool>> predicate,
        CancellationToken cancellationToken = default);
}
