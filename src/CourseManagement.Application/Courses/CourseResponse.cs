namespace CourseManagement.Application.Courses;

public sealed record CourseResponse(
    Guid Id,
    string Title,
    DateTime CreatedAt,
    string? Description,
    DateTime? UpdatedAt
);
