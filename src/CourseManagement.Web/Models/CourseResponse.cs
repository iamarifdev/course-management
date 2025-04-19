namespace CourseManagement.Web.Models;

public sealed record CourseResponse(
    Guid Id,
    string Title,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
