using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.GetClassCourses;

public sealed record GetClassCoursesQuery(Guid Id) : IQuery<ClassCoursesResponse>;
