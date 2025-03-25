using CourseManagement.Application.Base.Extensions;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class StudentRepository(ApplicationDbContext dbContext)
    : Repository<Student>(dbContext), IStudentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students.FirstOrDefaultAsync(
            s => s.User.Email == new Email(email.ToLowerCase()),
            cancellationToken
        );
    }
}
