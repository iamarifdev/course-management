using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students;

public static class StudentErrors
{
    public static readonly Error StudentNotFound = Error.NotFound(
        "Student.StudentNotFound",
        "The student with the specified identifier was not found");
}
