namespace CourseManagement.Application.Users;

public sealed record UserInfo(
    Guid Id,
    Guid UserId,
    string Email,
    string Role,
    string PasswordHash,
    string? FirstName,
    string? LastName,
    string? Department
);
