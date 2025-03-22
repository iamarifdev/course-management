using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.DeleteStudent;

public sealed record DeleteStudentCommand(Guid Id) : ICommand;
