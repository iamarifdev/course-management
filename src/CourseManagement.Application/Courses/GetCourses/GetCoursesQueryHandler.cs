using CourseManagement.Application.Base;
using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Courses.GetCourses;

internal sealed class GetCoursesQueryHandler(ICourseRepository repository)
    : IQueryHandler<GetCoursesQuery, PaginatedResult<CourseResponse>>
{
    public async Task<Result<PaginatedResult<CourseResponse>>> Handle(
        GetCoursesQuery request,
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
            .Select(c => new CourseResponse(c.Id, c.Name, c.CreatedAt, c.Description, c.UpdatedAt))
            .ToListAsync(cancellationToken);

        var paginatedResult = new PaginatedResult<CourseResponse>
        {
            Items = items,
            TotalCount = count,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
        };

        return Result.Success(paginatedResult);
    }
}
