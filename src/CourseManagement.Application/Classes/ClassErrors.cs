using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes;

public static class ClassErrors
{
    public static readonly Error ClassAlreadyExists = Error.Conflict(
        "Class.AlreadyExists",
        "A class with the same name already exists");

    public static readonly Error NoCourseAssociated = Error.Failure(
        "Class.NoCourseAssociated",
        "At least one course must be associated with the class");

    public static readonly Error NotFound = Error.NotFound(
        "Class.NotFound",
        "The class with the specified identifier was not found");
    
    public static Error InvalidCoursesAssociated(IEnumerable<Guid> ids) => Error.Failure(
        "Class.InvalidCoursesAssociated",
        $"The class is associated with invalid/missing courses: {string.Join(", ", ids)}");
    
    public static readonly Error StudentAlreadyEnrolled = Error.Conflict(
        "Class.StudentAlreadyEnrolled",
        "The student is already enrolled in the class");
}
