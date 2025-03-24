namespace CourseManagement.Application.Students.GetStudentCourseClasses;

public sealed record StudentCourseResponse(
    Guid Id,
    string Title,
    string? Description
);

public sealed record StudentClassResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime EnrolledAt
);

public sealed record StudentCourseClassResponse(
    Guid StudentId,
    StudentCourseResponse Course,
    List<StudentClassResponse> Classes
);
