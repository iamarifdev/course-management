using System.Linq.Expressions;
using CourseManagement.Application.Users;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Services;

public sealed class UserService(ApplicationDbContext dbContext) : IUserService
{
    public async Task<UserInfo?> GetUserInfoAsync(Expression<Func<User, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.AsQueryable().AsNoTracking()
            .Where(predicate)
            .Select(x => new UserInfo(
                x.Role == Role.Staff ? x.Staff!.Id : x.Student!.Id,
                x.Id,
                x.Email.Value,
                x.Role.ToString(),
                x.Password.Value,
                x.Role == Role.Staff ? x.Staff!.FirstName : x.Student!.FirstName,
                x.Role == Role.Staff ? x.Staff!.LastName : x.Student!.LastName,
                x.Role == Role.Staff ? x.Staff!.Department : null
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
