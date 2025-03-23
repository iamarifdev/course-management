using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.EnrollStudentInClass;

public record EnrollStudentInClassCommand(Guid ClassId, Guid StudentId, Guid StaffId) : ICommand;
