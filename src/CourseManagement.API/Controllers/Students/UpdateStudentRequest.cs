namespace CourseManagement.API.Controllers.Students;

public sealed record UpdateStudentRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password
);
