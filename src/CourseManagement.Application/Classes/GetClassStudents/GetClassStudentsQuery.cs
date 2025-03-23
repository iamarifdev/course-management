using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.GetClassStudents;

public sealed record GetClassStudentsQuery(Guid Id) : IQuery<ClassStudentsResponse>;
