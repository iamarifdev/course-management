using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses;

public static class CourseErrors
{
    public static readonly Error AlreadyExists = Error.Conflict(
        "Course.AlreadyExists",
        "A course with the same name already exists");

    public static readonly Error NotFound = Error.NotFound(
        "Course.NotFound",
        "The course with the specified identifier was not found");
    
    public static readonly Error StudentAlreadyEnrolled = Error.Conflict(
        "Course.StudentAlreadyEnrolled",
        "The student is already enrolled in the course");
}
