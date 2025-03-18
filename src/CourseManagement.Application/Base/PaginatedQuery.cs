namespace CourseManagement.Application.Base;

public interface IFilterableQuery
{
    string? FilterText { get; set; }
}

public interface ISortableQuery
{
    string? SortBy { get; set; }
    bool SortDescending { get; set; }
}

public abstract record PaginatedQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    public int SkipItems => (PageNumber - 1) * PageSize;
}
