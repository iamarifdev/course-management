using CourseManagement.Application.Base;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Application.Courses.GetCourseById;

internal sealed class GetCourseByIdQueryHandler(ICourseRepository repository)
    : IQueryHandler<GetCourseByIdQuery, CourseResponse>
{
    public async Task<Result<CourseResponse>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return Result.Failure<CourseResponse>(CourseErrors.NotFound);
        }

        return new CourseResponse(
            course.Id,
            course.Title,
            course.Description,
            course.CreatedAt,
            course.UpdatedAt
        );
    }
}
