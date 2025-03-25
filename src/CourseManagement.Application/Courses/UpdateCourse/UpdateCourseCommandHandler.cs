using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Application.Courses.UpdateCourse;

internal sealed class UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCourseCommand, CourseResponse>
{
    public async Task<Result<CourseResponse>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return Result.Failure<CourseResponse>(CourseErrors.NotFound);
        }
        
        // check whether the course name already exists
        var courseByName = await courseRepository.GetByNameAsync(request.Name, cancellationToken);
        if (courseByName is not null && courseByName.Id != course.Id)
        {
            return Result.Failure<CourseResponse>(CourseErrors.AlreadyExists);
        }

        course.Update(request.Name, request.Description);
        courseRepository.Update(course);

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
