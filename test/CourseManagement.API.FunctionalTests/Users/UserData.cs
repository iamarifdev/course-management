using CourseManagement.API.Controllers.Auth;

namespace CourseManagement.API.FunctionalTests.Users;

internal static class UserData
{
    public const string Email = "login@test.com";
    public const string Password = "12345";
    public const string FirstName = "Test";
    public const string LastName = "User";
    public const string Department = "Test Department";
    
    public static readonly LogInUserRequest LoginTestUserRequest = new(Email, Password);
    public static readonly LogInUserRequest InvalidLoginTestUserRequest = new("Email", "Password");
}
