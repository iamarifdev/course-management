using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.CreateClass;

public sealed record CreateClassCommand(string Name, List<Guid> CourseIds, string? Description) : ICommand<Guid>;
