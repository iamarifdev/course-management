using CourseManagement.Domain.Base;
using CourseManagement.Domain.CourseClasses;

namespace CourseManagement.Domain.Classes;

public class Class : Entity
{
    private Class(Guid id, string name, Guid createdBy, List<Guid> courseIds, string? description) : base(id)
    {
        Name = name;
        Description = description;
        CreatedBy = createdBy;
        CourseClasses = [.. courseIds.Select(courseId => CourseClass.Create(courseId, id, createdBy))];
    }

    private Class() { }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid CreatedBy { get; private set; }

    public virtual ICollection<CourseClass> CourseClasses { get; init; } = [];

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public static Class Create(string name, Guid createdBy, List<Guid> courseIds, string? description)
    {
        return new Class(Guid.NewGuid(), name, createdBy, courseIds, description);
    }
}
