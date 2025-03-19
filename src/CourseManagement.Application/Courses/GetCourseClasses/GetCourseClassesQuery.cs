using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.GetCourseClasses;

public sealed record GetCourseClassesQuery(Guid Id) : IQuery<CourseClassesResponse>;
