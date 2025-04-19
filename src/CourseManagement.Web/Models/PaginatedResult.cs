namespace CourseManagement.Web.Models;

public sealed record PaginatedResult<T>(
    List<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize,
    int TotalPages
);
