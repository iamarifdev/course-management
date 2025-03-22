using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.GetStudents;

public sealed record GetStudentsQuery : PaginatedQuery, IFilterableQuery, ISortableQuery,
    IQuery<PaginatedResult<StudentResponse>>
{
    public string? FilterText { get; set; }
    public string? SortBy { get; set; }
    public SortOrder? SortOrder { get; set; }
}
