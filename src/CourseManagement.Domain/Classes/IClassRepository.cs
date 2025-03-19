using System.Linq.Expressions;
using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Classes;

public interface IClassRepository : IRepository<Class>
{
    Task<Class?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
