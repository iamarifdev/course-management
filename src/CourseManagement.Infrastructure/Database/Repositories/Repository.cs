using CourseManagement.Domain.Users;

namespace CourseManagement.Infrastructure.Database.Repositories;

using Domain.Base;
using Microsoft.EntityFrameworkCore;

internal abstract class Repository<TEntity>(ApplicationDbContext dbContext) where TEntity : Entity
{
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public virtual void Add(TEntity entity) => dbContext.Add(entity);
}