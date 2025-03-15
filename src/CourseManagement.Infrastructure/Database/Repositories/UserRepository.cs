using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
