using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database;

public static class EntityTypeBuilderExtension
{
    /// <summary>
    /// The ApplicationDbContext uses snake case naming convention for columns i.e. IsDeleted -> is_deleted
    /// </summary>
    public static IndexBuilder<TEntity> HasIsDeletedFilter<TEntity>(this IndexBuilder<TEntity> indexBuilder)
    {
        return indexBuilder.HasFilter("\"is_deleted\" = false");
    }
}
