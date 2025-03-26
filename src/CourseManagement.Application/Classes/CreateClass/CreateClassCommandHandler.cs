using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Classes.CreateClass;

internal sealed class CreateClassCommandHandler(
    ICourseRepository courseRepository,
    IClassRepository classRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateClassCommand, ClassResponse>
{
    public async Task<Result<ClassResponse>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        if (!request.CourseIds.Any())
        {
            return Result.Failure<ClassResponse>(ClassErrors.NoCourseAssociated);
        }

        var courses = await courseRepository.GetQueryable()
            .Where(c => request.CourseIds.Contains(c.Id))
            .ToListAsync(cancellationToken);
        if (courses.Count != request.CourseIds.Count)
        {
            var invalidIds = request.CourseIds.Except(courses.Select(c => c.Id));
            return Result.Failure<ClassResponse>(ClassErrors.InvalidCoursesAssociated(invalidIds));
        }

        var isExists = await classRepository.ExistsAsync(x => x.Title == request.Name, cancellationToken);
        if (isExists)
        {
            return Result.Failure<ClassResponse>(ClassErrors.ClassAlreadyExists);
        }

        var @class = Class.Create(request.Name, request.CreatedById, request.CourseIds, request.Description);

        classRepository.Add(@class);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new ClassResponse(
            @class.Id,
            @class.Title,
            @class.Description,
            @class.CreatedAt,
            @class.UpdatedAt
        );

        return Result.Success(response);
    }
}
