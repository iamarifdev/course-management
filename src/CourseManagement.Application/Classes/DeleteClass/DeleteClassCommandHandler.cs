using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;

namespace CourseManagement.Application.Classes.DeleteClass;

internal sealed class DeleteClassCommandHandler(IClassRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteClassCommand>
{
    public async Task<Result> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return Result.Failure(ClassErrors.NotFound);
        }

        course.SetDeleted();
        repository.Update(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
