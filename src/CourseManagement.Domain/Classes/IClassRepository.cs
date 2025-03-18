namespace CourseManagement.Domain.Classes;

public interface IClassRepository
{
    IQueryable<Class> GetQueryable();
    Task<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Class?> GetByNameAsync(string name, CancellationToken cancellationToken);
    void Add(Class classEntity);
    void Update(Class classEntity);
}
