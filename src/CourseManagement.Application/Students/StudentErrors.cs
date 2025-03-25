using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students;

public static class StudentErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Student.NotFound",
        "The student with the specified identifier was not found");
    public static readonly Error NotEnrolledAnyClass = Error.NotFound(
        "Student.NotEnrolledAnyClass",
        "The student is not enrolled in any class");
    public static readonly Error NotEnrolledInClass = Error.NotFound(
        "Student.NotEnrolledInClass",
        "The student is not enrolled in the specified class");
    public static readonly Error AlreadyExistsByEmail = Error.Conflict(
        "Student.AlreadyExists",
        "The student with the specified email already exists");
}
