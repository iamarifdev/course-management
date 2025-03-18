using CourseManagement.Application.Exceptions;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.CourseClasses;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }
    
    public DbSet<CourseClass> CourseClasses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);

        modelBuilder.Entity<User>().HasQueryFilter(user => !user.IsDeleted);
        modelBuilder.Entity<Course>().HasQueryFilter(course => !course.IsDeleted);
        modelBuilder.Entity<Class>().HasQueryFilter(@class => !@class.IsDeleted);
        modelBuilder.Entity<CourseClass>().HasQueryFilter(courseClass => !courseClass.IsDeleted);

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
