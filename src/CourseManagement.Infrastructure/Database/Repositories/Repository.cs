using System.Linq.Expressions;
using CourseManagement.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal abstract class Repository<TEntity>(ApplicationDbContext dbContext) : IRepository<TEntity> where TEntity : Entity
{
    public IQueryable<TEntity> GetQueryable() => dbContext.Set<TEntity>().AsNoTracking().AsQueryable();

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }

    public virtual void Add(TEntity entity) => dbContext.Add(entity);
    public virtual void Update(TEntity entity) => dbContext.Update(entity);
}
