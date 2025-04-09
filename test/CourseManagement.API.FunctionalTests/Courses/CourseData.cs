using CourseManagement.API.Controllers.Courses;

namespace CourseManagement.API.FunctionalTests.Courses;

internal static class CourseData
{
    public const string Title = "Test Course";
    public const string Description = "Test Course Description";
    public static readonly CreateCourseRequest CreateCourseRequest = new(Title, Description);
}
