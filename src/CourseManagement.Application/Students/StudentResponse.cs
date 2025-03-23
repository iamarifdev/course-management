namespace CourseManagement.Application.Students;

public sealed record StudentResponse(
    Guid Id,
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    Guid StaffId,
    DateTime CreatedAt
);
