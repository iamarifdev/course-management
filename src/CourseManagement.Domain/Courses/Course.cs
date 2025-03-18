using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Courses;

public sealed class Course : Entity
{
    private Course(Guid id, string name, Guid createdBy, string? description) : base(id)
    {
        Name = name;
        Description = description;
        CreatedBy = createdBy;
    }

    private Course() { }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid CreatedBy { get; private set; }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public static Course Create(string name, Guid createdBy, string? description)
    {
        var course = new Course(Guid.NewGuid(), name, createdBy, description);
        return course;
    }
}
