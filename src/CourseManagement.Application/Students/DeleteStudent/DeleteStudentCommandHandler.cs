using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Students;

namespace CourseManagement.Application.Students.DeleteStudent;

internal sealed class DeleteStudentCommandHandler(IStudentRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteStudentCommand>
{
    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (student is null)
        {
            return Result.Failure(StudentErrors.StudentNotFound);
        }

        student.SetDeleted();
        repository.Update(student);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
