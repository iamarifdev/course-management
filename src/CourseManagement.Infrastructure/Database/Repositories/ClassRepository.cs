using CourseManagement.Domain.Classes;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class ClassRepository(ApplicationDbContext dbContext)
    : Repository<Class>(dbContext), IClassRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Class?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Classes.FirstOrDefaultAsync(c => c.Title == name, cancellationToken);
    }
}
