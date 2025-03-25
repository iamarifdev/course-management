namespace CourseManagement.Application.Students;

public sealed record StudentResponse(
    Guid Id,
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    Guid AddedById,
    DateTime CreatedAt
);
