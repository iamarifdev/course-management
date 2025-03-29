using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.GetClasses;

public sealed record GetClassesQuery(
    string? FilterText,
    int? PageNumber = 1,
    int? PageSize = 20
) : PaginatedQuery(PageNumber, PageSize), IFilterableQuery, IQuery<PaginatedResult<ClassResponse>>;
