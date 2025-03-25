using CourseManagement.Application.Base;
using CourseManagement.Application.Courses;
using CourseManagement.Domain.Classes;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Classes.GetClassCourses;

internal sealed class GetClassCoursesQueryHandler(IClassRepository repository)
    : IQueryHandler<GetClassCoursesQuery, ClassCoursesResponse>
{
    public async Task<Result<ClassCoursesResponse>> Handle(
        GetClassCoursesQuery request,
        CancellationToken cancellationToken
    )
    {
        var @class = await repository.GetQueryable()
            .Where(x => x.Id == request.Id)
            .Select(s => new ClassCoursesResponse(
                s.Id,
                s.Title,
                s.Description,
                s.CourseClasses.Select(c => new CourseResponse(
                    c.Course.Id,
                    c.Course.Title,
                    c.Course.Description,
                    c.Course.CreatedAt,
                    c.Course.UpdatedAt
                )).ToList(),
                s.CreatedAt,
                s.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return @class is null
            ? Result.Failure<ClassCoursesResponse>(ClassErrors.NotFound)
            : Result.Success(@class);
    }
}
