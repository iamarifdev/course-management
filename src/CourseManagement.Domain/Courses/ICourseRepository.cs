using CourseManagement.Domain.Courses.ValueObjects;

namespace CourseManagement.Domain.Courses;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Course?> GetByNameAsync(Name name, CancellationToken cancellationToken);
    void Add(Course course);
}
