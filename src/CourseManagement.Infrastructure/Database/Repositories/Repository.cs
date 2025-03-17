using CourseManagement.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal abstract class Repository<TEntity>(ApplicationDbContext dbContext) where TEntity : Entity
{
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public virtual void Add(TEntity entity) => dbContext.Add(entity);
}
