using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.GetStudents;

public sealed record GetStudentsQuery(
    string? FilterText,
    string? SortBy,
    SortOrder? SortOrder,
    int? PageNumber = 1,
    int? PageSize = 20
) : PaginatedQuery(PageNumber, PageSize), IFilterableQuery, ISortableQuery, IQuery<PaginatedResult<StudentResponse>>;
