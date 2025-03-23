using CourseManagement.Domain.StudentCourses;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class StudentCourseRepository(ApplicationDbContext dbContext)
    : Repository<StudentCourse>(dbContext), IStudentCourseRepository
{
}
