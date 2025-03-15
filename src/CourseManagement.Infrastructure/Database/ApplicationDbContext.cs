using CourseManagement.Application.Exceptions;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            SetUpdatedAt();
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency error occurred.", ex);
        }
    }

    private void SetUpdatedAt()
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdatedAt();
            }
        }
    }
}