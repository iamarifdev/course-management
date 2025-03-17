using CourseManagement.Domain.Base;

namespace CourseManagement.Domain.Courses;

public static class CourseErrors
{
    public static readonly Error CourseAlreadyExists = Error.Conflict(
        "Course.AlreadyExists",
        "A course with the same name already exists");
}
