using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.CreateStudent;

public sealed record CreateStudentCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<Guid>;
