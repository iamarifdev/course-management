using CourseManagement.Application.Base;

namespace CourseManagement.API.Controllers.Students;

public record GetAllStudentsRequest : PaginatedQuery, IFilterableQuery, ISortableQuery
{
    public string? FilterText { get; set; }
    public string? SortBy { get; set; }
    public SortOrder? SortOrder { get; set; }
}
