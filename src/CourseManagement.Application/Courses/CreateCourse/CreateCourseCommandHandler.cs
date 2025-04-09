using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Application.Courses.CreateCourse;

internal sealed class CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCourseCommand, CourseResponse>
{
    public async Task<Result<CourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.Create(request.Name, request.Description, request.CreatedById);

        var existingCourse = await courseRepository.GetByNameAsync(request.Name, cancellationToken);
        if (existingCourse is not null)
        {
            return Result.Failure<CourseResponse>(CourseErrors.AlreadyExists);
        }

        courseRepository.Add(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CourseResponse(
            course.Id,
            course.Title,
            course.Description,
            course.CreatedAt,
            course.UpdatedAt
        );

        return Result.Success(response);
    }
}
