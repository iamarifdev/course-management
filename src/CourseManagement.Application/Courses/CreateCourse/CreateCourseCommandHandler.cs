using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Courses.ValueObjects;

namespace CourseManagement.Application.Courses.CreateCourse;

internal sealed class CreateCourseCommandHandler(
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    ICourseRepository courseRepository)
    : ICommandHandler<CreateCourseCommand, CourseResponse>
{
    public async Task<Result<CourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var createdBy = userContext.UserId;
        var name = new Name(request.Name);
        var description = request.Description is null ? null : new Description(request.Description);

        var course = new Course(name, createdBy, description);
        
        var existingCourse = await courseRepository.GetByNameAsync(name, cancellationToken);
        if (existingCourse is not null)
        {
            return Result.Failure<CourseResponse>(CourseErrors.CourseAlreadyExists);
        }

        courseRepository.Add(course);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CourseResponse(course.Id);
    }
}
