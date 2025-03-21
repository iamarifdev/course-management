namespace CourseManagement.API.Controllers.Students;

public sealed record CreateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);
