using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.GetClassById;

public sealed record GetClassByIdQuery(Guid Id) : IQuery<ClassResponse>;
