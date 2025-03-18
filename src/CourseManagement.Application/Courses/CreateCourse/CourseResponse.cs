namespace CourseManagement.Application.Courses.CreateCourse;

public sealed record CourseResponse(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    string? Description,
    DateTime? UpdatedAt
);
