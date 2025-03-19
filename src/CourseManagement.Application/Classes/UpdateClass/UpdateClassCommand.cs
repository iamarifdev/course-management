using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.UpdateClass;

public sealed record UpdateClassCommand(Guid Id, string Name, string? Description) : ICommand<ClassResponse>;
