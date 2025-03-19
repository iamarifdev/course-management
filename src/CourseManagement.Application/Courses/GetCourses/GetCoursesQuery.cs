using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.GetCourses;

public record GetCoursesQuery : PaginatedQuery, IFilterableQuery, IQuery<PaginatedResult<CourseResponse>>
{
    public string? FilterText { get; set; }
}
