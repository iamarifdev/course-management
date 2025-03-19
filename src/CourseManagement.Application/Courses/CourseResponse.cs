namespace CourseManagement.Application.Courses;

public sealed record CourseResponse(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    string? Description,
    DateTime? UpdatedAt
);
