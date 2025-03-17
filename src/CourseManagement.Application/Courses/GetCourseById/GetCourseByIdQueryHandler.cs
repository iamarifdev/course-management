using CourseManagement.Application.Base;
using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Domain.Base;
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
            return Result.Failure<CourseResponse>(CourseErrors.CourseNotFound);
        }

        return new CourseResponse(
            course.Id,
            course.Name.Value,
            course.Description?.Value
        );
    }
}
