using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.CreateCourse;

public record CreateCourseCommand(string Name, string? Description) : ICommand<Guid>;
