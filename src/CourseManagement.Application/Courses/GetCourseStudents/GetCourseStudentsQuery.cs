using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.GetCourseStudents;

public sealed record GetCourseStudentsQuery(Guid Id) : IQuery<CourseStudentsResponse>;
