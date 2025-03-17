namespace CourseManagement.Application.Courses;

public static class CourseErrorCodes
{
    public static class CreateCourse
    {
        public const string EmptyName = nameof(EmptyName);
        public const string InvalidName = nameof(InvalidName);
        public const string NameMaxLengthExceeds = nameof(NameMaxLengthExceeds);
        public const string DescriptionMaxLengthExceeds = nameof(DescriptionMaxLengthExceeds);
    }
}
