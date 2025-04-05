using CourseManagement.API.Binders;
using CourseManagement.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Students;

public record GetAllStudentsRequest(
    string? FilterText,
    string? SortBy,
    [ModelBinder(BinderType = typeof(SortOrderBinder))]
    SortOrder? SortOrder,
    int? PageNumber = 1,
    int? PageSize = 20
) : PaginatedQuery(PageNumber, PageSize), IFilterableQuery, ISortableQuery;
