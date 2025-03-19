using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.DeleteClass;

public sealed record DeleteClassCommand(Guid Id) : ICommand;
