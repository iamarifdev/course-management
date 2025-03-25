using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.UpdateStudent;

public sealed record UpdateStudentCommand(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password
) : ICommand<StudentResponse>;
