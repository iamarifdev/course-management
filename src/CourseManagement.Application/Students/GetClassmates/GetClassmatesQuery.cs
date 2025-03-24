using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.GetClassmates;

public sealed record GetClassmatesQuery(Guid StudentId) : IQuery<List<ClassmatesResponse>>;
