namespace CourseManagement.Application.Base;

public class PaginatedResult<T> where T : class
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; init; }
    
    public int PageNumber { get; set; }
    public int PageSize { get; init; }
    
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
