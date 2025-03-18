namespace CourseManagement.Domain.Courses;

public interface ICourseRepository
{
    IQueryable<Course> GetQueryable();
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Course?> GetByNameAsync(string name, CancellationToken cancellationToken);
    void Add(Course course);
    void Update(Course course);
}
