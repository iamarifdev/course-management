using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.GetStudentById;

public sealed record GetStudentByIdQuery(Guid Id) : IQuery<StudentResponse>;
