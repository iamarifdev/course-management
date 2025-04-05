using CourseManagement.Application.Base;
using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Courses.GetCourses;

internal sealed class GetCoursesQueryHandler(ICourseRepository repository)
    : IQueryHandler<GetCoursesQuery, PaginatedResult<CourseResponse>>
{
    public async Task<Result<PaginatedResult<CourseResponse>>> Handle(
        GetCoursesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = repository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(request.FilterText))
        {
            query = query.Where(c => c.Title.Contains(request.FilterText));
        }

        query = query.OrderBy(c => c.Title);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.GetSkipItems())
            .Take(request.GetItemsCount())
            .Select(c => new CourseResponse(c.Id, c.Title, c.Description, c.CreatedAt, c.UpdatedAt))
            .ToListAsync(cancellationToken);

        return Result.Success(items.ToPaginatedResult(totalCount, request));
    }
}
