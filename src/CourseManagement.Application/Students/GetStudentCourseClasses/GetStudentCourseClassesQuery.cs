using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.GetStudentCourseClasses;

public sealed record GetStudentCourseClassesQuery(Guid StudentId) : IQuery<List<StudentCourseClassResponse>>;
