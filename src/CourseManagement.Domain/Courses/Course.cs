using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses.ValueObjects;

namespace CourseManagement.Domain.Courses;

public sealed class Course : Entity
{
    public Course(Name name, Guid createdBy, Description? description)
    {
        Name = name;
        Description = description;
        CreatedBy = createdBy;
    }

    private Course() { }

    public Name Name { get; private set; }
    public Description? Description { get; private set; }
    public Guid CreatedBy { get; private set; }
}
