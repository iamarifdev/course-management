namespace CourseManagement.Application.Classes;

public sealed record ClassResponse(
    Guid Id,
    string Title,
    DateTime CreatedAt,
    string? Description,
    DateTime? UpdatedAt
);
