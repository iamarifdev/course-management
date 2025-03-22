using CourseManagement.Application.Base;
using CourseManagement.Domain.Classes;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Classes.GetClasses;

internal sealed class GetClassesQueryHandler(IClassRepository repository)
    : IQueryHandler<GetClassesQuery, PaginatedResult<ClassResponse>>
{
    public async Task<Result<PaginatedResult<ClassResponse>>> Handle(
        GetClassesQuery request,
        CancellationToken cancellationToken)
    {
        var query = repository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(request.FilterText))
        {
            query = query.Where(c => c.Title.Contains(request.FilterText));
        }

        query = query.OrderBy(c => c.Title);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.SkipItems)
            .Take(request.PageSize)
            .Select(c => new ClassResponse(c.Id, c.Title, c.CreatedAt, c.Description, c.UpdatedAt))
            .ToListAsync(cancellationToken);

        return Result.Success(items.ToPaginatedResult(totalCount, request));
    }
}
