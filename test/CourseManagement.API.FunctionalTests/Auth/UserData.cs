using CourseManagement.API.Controllers.Auth;

namespace CourseManagement.API.FunctionalTests.Auth;

internal static class UserData
{
    internal static class Student
    {
        public const string Email = "student@test.com";
        public const string Password = "12345";
        public const string FirstName = "Test";
        public const string LastName = "Student";
    }

    internal static class Staff
    {
        public const string Email = "stuff@test.com";
        public const string Password = "12345";
        public const string FirstName = "Test";
        public const string LastName = "Staff";
        public const string Department = "Test Department";
    }
    
    public static readonly LogInUserRequest LoginTestUserRequest = new(Staff.Email, Staff.Password);
    public static readonly LogInUserRequest InvalidLoginTestUserRequest = new("Email", "Password");
}
