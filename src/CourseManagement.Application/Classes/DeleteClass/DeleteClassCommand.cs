using CourseManagement.Application.Base;
using CourseManagement.Application.Courses;
using CourseManagement.Application.Courses.DeleteCourse;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Application.Classes.DeleteClass;

public sealed record DeleteClassCommand(Guid Id) : ICommand;
