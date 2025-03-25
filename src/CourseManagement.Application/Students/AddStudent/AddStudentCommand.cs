using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.AddStudent;

public sealed record AddStudentCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid AddedById
) : ICommand<StudentResponse>;
