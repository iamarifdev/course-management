namespace CourseManagement.Domain.Base;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    // EF Core needs this constructor in Migration
    protected Entity()
    {
    }

    public Guid Id { get; init; }
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
    
    public void SetDeleted() => IsDeleted = true;
}