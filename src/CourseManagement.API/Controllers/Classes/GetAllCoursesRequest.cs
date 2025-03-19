using CourseManagement.Application.Base;

namespace CourseManagement.API.Controllers.Classes;

public sealed record GetAllClassesRequest : PaginatedQuery, IFilterableQuery
{
    public string? FilterText { get; set; }
}
