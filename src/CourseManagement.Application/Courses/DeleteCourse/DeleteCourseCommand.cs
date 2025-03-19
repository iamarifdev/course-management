using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.DeleteCourse;

public sealed record DeleteCourseCommand(Guid Id) : ICommand;
