using CourseManagement.Domain.Base;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.StudentCourseClasses;
using CourseManagement.Domain.StudentCourses;
using CourseManagement.Domain.Users;

namespace CourseManagement.Domain.Students;

public sealed class Student : Entity
{
    private Student(Guid id, Guid userId, string firstName, string lastName, Guid staffId) : base(id)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        StaffId = staffId;
    }

    private Student() { }

    public Guid UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid StaffId { get; private set; }

    public User User { get; init; }
    public Staff AddedBy { get; init; }
    public ICollection<StudentCourseClass> EnrolledClasses { get; init; } = [];
    public ICollection<StudentCourse> EnrolledCourses { get; init; } = [];

    public static Student Create(Guid userId, string firstName, string lastName, Guid staffId)
    {
        return new Student(Guid.NewGuid(), userId, firstName, lastName, staffId);
    }

    public void Update(string? firstName, string? lastName)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            FirstName = firstName;
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            LastName = lastName;
        }
    }
}
