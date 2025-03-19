using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.UpdateCourse;

public sealed record UpdateCourseCommand(Guid Id, string Name, string? Description) : ICommand<CourseResponse>;
