using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.CourseClasses;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.StudentCourseClasses;
using CourseManagement.Domain.StudentCourses;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;

namespace CourseManagement.Domain.Staffs;

public sealed class Staff : Entity
{
    private Staff(Guid id, Guid userId, string? firstName, string? lastName, Guid? staffId,
        string department) : base(id)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        StaffId = staffId;
        Department = department;
    }

    public Guid UserId { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string Department { get; private set; }
    public Guid? StaffId { get; private set; }

    public User User { get; set; }
    public Staff? AddedBy { get; set; }
    public ICollection<Course> Courses { get; init; } = [];
    public ICollection<Class> Classes { get; init; } = [];
    public ICollection<CourseClass> CourseClasses { get; init; } = [];
    public ICollection<Student> Students { get; init; } = [];
    public ICollection<StudentCourse> EnrolledStudentCourses { get; init; } = [];
    public ICollection<StudentCourseClass> EnrolledStudentClasses { get; init; } = [];

    public static Staff Create(Guid userId, string? firstName, string? lastName, Guid? staffId, string department)
    {
        return new Staff(Guid.NewGuid(), userId, firstName, lastName, staffId, department);
    }
}
