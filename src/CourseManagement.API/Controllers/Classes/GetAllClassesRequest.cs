using CourseManagement.Application.Base;

namespace CourseManagement.API.Controllers.Classes;

public sealed record GetAllClassesRequest(string? FilterText, int? PageNumber = 1, int? PageSize = 20)
    : PaginatedQuery(PageNumber, PageSize), IFilterableQuery;
