namespace CourseManagement.Application.Students;

public static class StudentValidatorErrorCodes
{
    public const string EmptyFirstName = nameof(EmptyFirstName);
    public const string FirstNameMaxLengthExceeds = nameof(FirstNameMaxLengthExceeds);
    public const string EmptyLastName = nameof(EmptyFirstName);
    public const string LastNameMaxLengthExceeds = nameof(LastNameMaxLengthExceeds);
    public const string EmptyEmail = nameof(EmptyEmail);
    public const string InvalidEmail = nameof(InvalidEmail);
    public const string EmptyPassword = nameof(EmptyPassword);
    public const string PasswordMaxLengthExceeds = nameof(PasswordMaxLengthExceeds);
}
