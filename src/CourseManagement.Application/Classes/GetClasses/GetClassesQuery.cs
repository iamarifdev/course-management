using CourseManagement.Application.Base;

namespace CourseManagement.Application.Classes.GetClasses;

public record GetClassesQuery : PaginatedQuery, IFilterableQuery, IQuery<PaginatedResult<ClassResponse>>
{
    public string? FilterText { get; set; }
}
