using CourseManagement.Domain.Users;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
}