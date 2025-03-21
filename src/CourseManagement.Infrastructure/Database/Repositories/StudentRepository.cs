using CourseManagement.Application.Students;
using CourseManagement.Domain.Students;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class StudentRepository(ApplicationDbContext dbContext)
    : Repository<Student>(dbContext), IStudentRepository
{
}
