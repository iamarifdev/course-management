namespace CourseManagement.Application.Classes;

public sealed record ClassResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
