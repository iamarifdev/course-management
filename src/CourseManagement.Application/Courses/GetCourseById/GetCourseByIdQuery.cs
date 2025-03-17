using CourseManagement.Application.Base;
using CourseManagement.Application.Courses.CreateCourse;

namespace CourseManagement.Application.Courses.GetCourseById;

public sealed record GetCourseByIdQuery(Guid Id) : IQuery<CourseResponse>;
