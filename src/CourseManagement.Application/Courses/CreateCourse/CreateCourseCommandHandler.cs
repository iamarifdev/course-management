using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Application.Courses.CreateCourse;

internal sealed class CreateCourseCommandHandler(
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    ICourseRepository courseRepository)
    : ICommandHandler<CreateCourseCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.Create(request.Name, userContext.StaffId, request.Description);

        var existingCourse = await courseRepository.GetByNameAsync(request.Name, cancellationToken);
        if (existingCourse is not null)
        {
            return Result.Failure<Guid>(CourseErrors.AlreadyExists);
        }

        courseRepository.Add(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(course.Id);
    }
}
