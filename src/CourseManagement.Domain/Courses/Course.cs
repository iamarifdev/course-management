using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Courses;

public sealed class Course : Entity
{
    public Course(string name, Guid createdBy, string? description)
    {
        Name = name;
        Description = description;
        CreatedBy = createdBy;
    }

    private Course() { }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid CreatedBy { get; private set; }
}
