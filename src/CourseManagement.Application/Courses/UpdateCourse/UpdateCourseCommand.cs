using CourseManagement.Application.Base;
using CourseManagement.Application.Courses.CreateCourse;

namespace CourseManagement.Application.Courses.UpdateCourse;

public sealed record UpdateCourseCommand(Guid Id, string Name, string? Description) : ICommand<CourseResponse>;
