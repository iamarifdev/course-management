using System.Linq.Expressions;

namespace CourseManagement.Domain.Base;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> GetQueryable();
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    void Add(TEntity entity);
    void Update(TEntity entity);
}
