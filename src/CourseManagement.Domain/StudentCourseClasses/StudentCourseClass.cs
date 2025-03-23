using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;

namespace CourseManagement.Domain.StudentCourseClasses;

public sealed class StudentCourseClass : Entity
{
    private StudentCourseClass(Guid id, Guid studentId, Guid courseId, Guid classId, Guid staffId) : base(id)
    {
        StudentId = studentId;
        CourseId = courseId;
        ClassId = classId;
        StaffId = staffId;
    }

    private StudentCourseClass() { }

    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid ClassId { get; private set; }
    public Guid StaffId { get; private set; }

    public Student Student { get; init; }
    public Course Course { get; init; }
    public Class Class { get; init; }
    public Staff EnrolledBy { get; init; }

    public static StudentCourseClass Create(Guid studentId, Guid courseId, Guid classId, Guid stuffId)
    {
        return new StudentCourseClass(Guid.NewGuid(), studentId, courseId, classId, stuffId);
    }
}
