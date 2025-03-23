using CourseManagement.Domain.StudentCourseClasses;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class StudentCourseClassRepository(ApplicationDbContext dbContext)
    : Repository<StudentCourseClass>(dbContext), IStudentCourseClassRepository
{
}
