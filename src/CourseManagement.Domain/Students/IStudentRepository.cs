using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Students;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
