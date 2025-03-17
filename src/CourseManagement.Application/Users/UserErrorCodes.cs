namespace CourseManagement.Application.Users;

public static class UserErrorCodes
{
    public static class LoginUser
    {
        public const string EmptyEmail = nameof(EmptyEmail);
        public const string InvalidEmail = nameof(InvalidEmail);
        public const string EmptyPassword = nameof(EmptyPassword);
    }
}
