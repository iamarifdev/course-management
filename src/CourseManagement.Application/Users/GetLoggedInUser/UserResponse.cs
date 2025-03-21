namespace CourseManagement.Application.Users.GetLoggedInUser;

public sealed record UserResponse(
    Guid Id,
    Guid UserId,
    string Email,
    string? FirstName,
    string? LastName,
    string? Department);
