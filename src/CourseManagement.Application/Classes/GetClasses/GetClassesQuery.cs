using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.GetClasses;

public sealed record GetClassesQuery : PaginatedQuery, IFilterableQuery, IQuery<PaginatedResult<ClassResponse>>
{
    public string? FilterText { get; set; }
}
