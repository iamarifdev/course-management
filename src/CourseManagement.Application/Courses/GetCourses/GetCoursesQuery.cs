using CourseManagement.Application.Base;
using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Domain.Base;

namespace CourseManagement.Application.Courses.GetCourses;

public record GetCoursesQuery : PaginatedQuery, IFilterableQuery, IQuery<PaginatedResult<CourseResponse>>
{
    public string? FilterText { get; set; }
}
