using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;

namespace CourseManagement.Domain.StudentCourses;

public sealed class StudentCourse : Entity
{
    private StudentCourse(Guid id, Guid studentId, Guid courseId, Guid staffId) : base(id)
    {
        StudentId = studentId;
        CourseId = courseId;
        StaffId = staffId;
    }

    private StudentCourse() { }

    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid StaffId { get; private set; }

    public Student Student { get; init; }
    public Course Course { get; init; }
    public Staff EnrolledBy { get; init; }

    public static StudentCourse Create(Guid studentId, Guid courseId, Guid stuffId)
    {
        return new StudentCourse(Guid.NewGuid(), studentId, courseId, stuffId);
    }
}
