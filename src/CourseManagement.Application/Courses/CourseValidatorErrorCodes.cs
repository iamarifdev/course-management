namespace CourseManagement.Application.Courses;

public static class CourseValidatorErrorCodes
{
    public const string EmptyId = nameof(EmptyId);
    public const string EmptyStudentId = nameof(EmptyStudentId);
    public const string EmptyCourseId = nameof(EmptyCourseId);
    public const string EmptyStaffId = nameof(EmptyStaffId);
    public const string EmptyName = nameof(EmptyName);
    public const string InvalidName = nameof(InvalidName);
    public const string NameMaxLengthExceeds = nameof(NameMaxLengthExceeds);
    public const string DescriptionMaxLengthExceeds = nameof(DescriptionMaxLengthExceeds);
}
