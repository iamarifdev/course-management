using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Domain.CourseClasses;

public class CourseClass : Entity
{
    private CourseClass(Guid id, Guid courseId, Guid classId, Guid createdBy) : base(id)
    {
        CourseId = courseId;
        ClassId = classId;
        CreatedBy = createdBy;
    }

    private CourseClass() { }

    public Guid CourseId { get; private set; }
    public Guid ClassId { get; private set; }
    public Guid CreatedBy { get; private set; }
    
    public virtual Course Course { get; set; } = null!;
    public virtual Class Class { get; set; } = null!;

    public static CourseClass Create(Guid courseId, Guid classId, Guid createdBy)
    {
        return new CourseClass(Guid.NewGuid(), courseId, classId, createdBy);
    }
}

