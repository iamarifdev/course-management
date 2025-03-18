using CourseManagement.Application.Base;

namespace CourseManagement.API.Controllers.Courses;

public sealed record GetAllCoursesRequest : PaginatedQuery, IFilterableQuery
{
    public string? FilterText { get; set; }
}
