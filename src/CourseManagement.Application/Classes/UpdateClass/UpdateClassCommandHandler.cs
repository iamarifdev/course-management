using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;

namespace CourseManagement.Application.Classes.UpdateClass;

internal sealed class UpdateClassCommandHandler(IClassRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateClassCommand, ClassResponse>
{
    public async Task<Result<ClassResponse>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {
        var @class = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (@class is null)
        {
            return Result.Failure<ClassResponse>(ClassErrors.ClassNotFound);
        }

        // check whether the @class name already exists
        var classByName = await repository.GetByNameAsync(request.Name, cancellationToken);
        if (classByName is not null && classByName.Id != @class.Id)
        {
            return Result.Failure<ClassResponse>(ClassErrors.ClassAlreadyExists);
        }

        @class.Update(request.Name, request.Description);
        repository.Update(@class);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new ClassResponse(
            @class.Id,
            @class.Title,
            @class.CreatedAt,
            @class.Description,
            @class.UpdatedAt
        );

        return Result.Success(result);
    }
}
