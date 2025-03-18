using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;

namespace CourseManagement.Application.Courses.DeleteCourse;

public sealed record DeleteCourseCommand(Guid Id) : ICommand;

internal sealed class DeleteCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCourseCommand>
{
    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return Result.Failure(CourseErrors.CourseNotFound);
        }

        course.SetDeleted();
        courseRepository.Update(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
