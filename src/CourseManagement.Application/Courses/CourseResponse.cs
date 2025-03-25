namespace CourseManagement.Application.Courses;

public sealed record CourseResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
