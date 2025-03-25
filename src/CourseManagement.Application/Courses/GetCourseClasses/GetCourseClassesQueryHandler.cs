using CourseManagement.Application.Base;
using CourseManagement.Application.Classes;
using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Courses.GetCourseClasses;

internal sealed class GetCourseClassesQueryHandler(ICourseRepository repository)
    : IQueryHandler<GetCourseClassesQuery, CourseClassesResponse>
{
    public async Task<Result<CourseClassesResponse>> Handle(
        GetCourseClassesQuery request,
        CancellationToken cancellationToken
    )
    {
        var course = await repository.GetQueryable()
            .Where(x => x.Id == request.Id)
            .Select(s => new CourseClassesResponse(
                s.Id,
                s.Title,
                s.Description,
                s.CourseClasses.Select(c => new ClassResponse(
                    c.Class.Id,
                    c.Class.Title,
                    c.Class.Description,
                    c.Class.CreatedAt,
                    c.Class.UpdatedAt
                )).ToList(),
                s.CreatedAt,
                s.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return course is null
            ? Result.Failure<CourseClassesResponse>(CourseErrors.NotFound)
            : Result.Success(course);
    }
}
