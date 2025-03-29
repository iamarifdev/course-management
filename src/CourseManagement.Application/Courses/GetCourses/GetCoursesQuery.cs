using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.GetCourses;

public record GetCoursesQuery(
    string? FilterText,
    int? PageNumber = 1,
    int? PageSize = 20
) : PaginatedQuery(PageNumber, PageSize), IFilterableQuery, IQuery<PaginatedResult<CourseResponse>>;
