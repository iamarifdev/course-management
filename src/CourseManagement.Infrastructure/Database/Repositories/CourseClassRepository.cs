using CourseManagement.Domain.CourseClasses;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class CourseClassRepository(ApplicationDbContext dbContext)
    : Repository<CourseClass>(dbContext), ICourseClassRepository
{
}
