using CourseManagement.Domain.Base;
using CourseManagement.Domain.CourseClasses;
using CourseManagement.Domain.Staffs;

namespace CourseManagement.Domain.Courses;

public sealed class Course : Entity
{
    private Course(Guid id, string title, Guid staffId, string? description) : base(id)
    {
        Title = title;
        Description = description;
        StaffId = staffId;
        CourseClasses = [];
    }

    private Course() { }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public Guid StaffId { get; private set; }
    
    public Staff CreatedBy { get; init; } = null!;
    public ICollection<CourseClass> CourseClasses { get; init; } = [];

    public void Update(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public static Course Create(string name, Guid createdBy, string? description)
    {
        return new Course(Guid.NewGuid(), name, createdBy, description);
    }
}
