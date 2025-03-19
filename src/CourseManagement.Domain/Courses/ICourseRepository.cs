using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Courses;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
