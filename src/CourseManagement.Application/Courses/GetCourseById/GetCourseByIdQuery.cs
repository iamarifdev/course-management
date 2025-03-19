using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.GetCourseById;

public sealed record GetCourseByIdQuery(Guid Id) : IQuery<CourseResponse>;
