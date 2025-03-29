using CourseManagement.Domain.Base;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.UnitTests.Users;

internal static class UserData
{
    internal static class StudentData
    {
        public const string Email = "student@test.com";
        public const string Password = "12345";
        public const string FirstName = "Test";
        public const string LastName = "Student";
    }

    public static Guid UserId { get; private set; }
    public static Guid StaffId => Guid.NewGuid();

    public static User CreateStudentUser()
    {
        var user = User.Create(
            new Email(StudentData.Email),
            Role.Student,
            new Password(StudentData.Password)
        );
        UserId = user.Id;
        return user;
    }

    public static Student AddStudent() => Student.Create(
        UserId,
        StudentData.FirstName,
        StudentData.LastName,
        StaffId
    );
}
