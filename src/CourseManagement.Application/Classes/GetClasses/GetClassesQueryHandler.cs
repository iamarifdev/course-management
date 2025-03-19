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
            query = query.Where(c => c.Name.Contains(request.FilterText));
        }

        query = query.OrderBy(c => c.Name);

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.SkipItems)
            .Take(request.PageSize)
            .Select(c => new ClassResponse(c.Id, c.Name, c.CreatedAt, c.Description, c.UpdatedAt))
            .ToListAsync(cancellationToken);

        var paginatedResult = new PaginatedResult<ClassResponse>
        {
            Items = items,
            TotalCount = count,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        return Result.Success(paginatedResult);
    }
}
