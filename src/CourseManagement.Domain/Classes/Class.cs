using CourseManagement.Domain.Base;
using CourseManagement.Domain.CourseClasses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.StudentCourseClasses;

namespace CourseManagement.Domain.Classes;

public sealed class Class : Entity
{
    private Class(Guid id, string title, Guid staffId, List<Guid> courseIds, string? description) : base(id)
    {
        Title = title;
        Description = description;
        StaffId = staffId;
        CourseClasses = [.. courseIds.Select(courseId => CourseClass.Create(courseId, id, staffId))];
    }

    private Class() { }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public Guid StaffId { get; private set; }
    
    public Staff CreatedBy { get; init; } = null!;
    public ICollection<CourseClass> CourseClasses { get; init; } = [];
    public ICollection<StudentCourseClass> EnrolledStudentClasses { get; init; } = [];

    public void Update(string title, string? description)
    {
        Title = title;
        Description = description;
    }

    public static Class Create(string title, Guid staffId, List<Guid> courseIds, string? description)
    {
        return new Class(Guid.NewGuid(), title, staffId, courseIds, description);
    }
}
