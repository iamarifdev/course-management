namespace CourseManagement.Application.Base;

public enum SortOrder
{
    Asc,
    Desc
}

public interface IFilterableQuery
{
    string? FilterText { get; init; }
}

public interface ISortableQuery
{
    string? SortBy { get; init; }
    SortOrder? SortOrder { get; init; }
}

public abstract record PaginatedQuery(int? PageNumber, int? PageSize)
{
    public int SkipItems
    {
        get
        {
            var pageNumber = PageNumber ?? 1;
            var pageSize = PageSize ?? 20;
            return (pageNumber - 1) * pageSize;
        }
    }
    public int CurrentPage => PageNumber ?? 1;
    public int ItemsCount => PageSize ?? 20;
}
