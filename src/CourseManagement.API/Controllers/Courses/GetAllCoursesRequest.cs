using CourseManagement.Application.Base;

namespace CourseManagement.API.Controllers.Courses;

public sealed record GetAllCoursesRequest(string? FilterText, int? PageNumber = 1, int? PageSize = 20)
    : PaginatedQuery(PageNumber, PageSize), IFilterableQuery;
