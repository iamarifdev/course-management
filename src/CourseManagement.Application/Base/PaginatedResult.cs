namespace CourseManagement.Application.Base;

public class PaginatedResult<T> where T : class
{
    protected PaginatedResult() { }
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; init; }

    public int PageNumber { get; set; }
    public int PageSize { get; init; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public static PaginatedResult<T> Create(List<T> items, int totalCount, PaginatedQuery request)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(request);

        return new PaginatedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.GetCurrentPage(),
            PageSize = request.GetItemsCount()
        };
    }
}

public static class PaginatedResultExtensions
{
    public static PaginatedResult<T> ToPaginatedResult<T>(this List<T> items, int totalCount, PaginatedQuery request)
        where T : class
    {
        return PaginatedResult<T>.Create(items, totalCount, request);
    }
}
