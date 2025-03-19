namespace CourseManagement.Application.Classes;

public sealed record ClassResponse(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    string? Description,
    DateTime? UpdatedAt
);
