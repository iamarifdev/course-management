using System.Linq.Expressions;

namespace CourseManagement.Application.Base.Extensions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> SortBy<T, TKey>(
        this IQueryable<T> source,
        Expression<Func<T, TKey>> keySelector,
        SortOrder? sortOrder
    )
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentNullException.ThrowIfNull(keySelector);

        return sortOrder switch
        {
            SortOrder.Asc => source.OrderBy(keySelector),
            SortOrder.Desc => source.OrderByDescending(keySelector),
            _ => source.OrderBy(keySelector),
        };
    }

    public static IOrderedQueryable<T> ThenSortBy<T, TKey>(
        this IOrderedQueryable<T> source,
        Expression<Func<T, TKey>> keySelector,
        SortOrder? sortOrder
    )
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentNullException.ThrowIfNull(keySelector);

        return sortOrder switch
        {
            SortOrder.Asc => source.ThenBy(keySelector),
            SortOrder.Desc => source.ThenByDescending(keySelector),
            _ => source.OrderBy(keySelector),
        };
    }
}
