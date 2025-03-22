namespace CourseManagement.Application.Base;

public enum SortOrder
{
    Asc,
    Desc
}

public interface IFilterableQuery
{
    string? FilterText { get; set; }
}

public interface ISortableQuery
{
    string? SortBy { get; set; }
    SortOrder? SortOrder { get; set; }
}

public abstract record PaginatedQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    public int SkipItems => (PageNumber - 1) * PageSize;
}
