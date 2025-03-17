using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class CourseRepository(ApplicationDbContext dbContext)
    : Repository<Course>(dbContext), ICourseRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Course?> GetByNameAsync(Name name, CancellationToken cancellationToken)
    {
        return await _dbContext.Courses.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }
}
