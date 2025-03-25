namespace CourseManagement.Application.Classes;

public static class ClassValidatorErrorCodes
{
    public const string EmptyId = nameof(EmptyId);
    public const string EmptyStudentId = nameof(EmptyStudentId);
    public const string EmptyClassId = nameof(EmptyClassId);
    public const string EmptyStaffId = nameof(EmptyStaffId);
    public const string EmptyName = nameof(EmptyName);
    public const string EmptyCourseIds = nameof(EmptyCourseIds);
    public const string InvalidName = nameof(InvalidName);
    public const string NameMaxLengthExceeds = nameof(NameMaxLengthExceeds);
    public const string EmptyDescription = nameof(EmptyDescription);
    public const string DescriptionMaxLengthExceeds = nameof(DescriptionMaxLengthExceeds);
    public const string EmptyCreatedById = nameof(EmptyCreatedById);
}
