using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;

namespace CourseManagement.Domain.CourseClasses;

public sealed class CourseClass : Entity
{
    private CourseClass(Guid id, Guid courseId, Guid classId, Guid staffId) : base(id)
    {
        CourseId = courseId;
        ClassId = classId;
        StaffId = staffId;
    }

    private CourseClass() { }

    public Guid CourseId { get; private set; }
    public Guid ClassId { get; private set; }
    public Guid StaffId { get; private set; }
    
    public Course Course { get; init; }
    public Class Class { get; init; }
    public Staff CreatedBy { get; init; }

    public static CourseClass Create(Guid courseId, Guid classId, Guid stuffId)
    {
        return new CourseClass(Guid.NewGuid(), courseId, classId, stuffId);
    }
}

