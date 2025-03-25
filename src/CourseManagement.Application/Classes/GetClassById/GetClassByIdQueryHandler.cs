using CourseManagement.Application.Base;
using CourseManagement.Domain.Classes;

namespace CourseManagement.Application.Classes.GetClassById;

internal sealed class GetClassByIdQueryHandler(IClassRepository repository)
    : IQueryHandler<GetClassByIdQuery, ClassResponse>
{
    public async Task<Result<ClassResponse>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var @class = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (@class is null)
        {
            return Result.Failure<ClassResponse>(ClassErrors.NotFound);
        }
        
        return new ClassResponse(
            @class.Id,
            @class.Title,
            @class.Description,
            @class.CreatedAt,
            @class.UpdatedAt
        );
    }
}
