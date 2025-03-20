using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Classes.CreateClass;

internal sealed class CreateClassCommandHandler(
    ICourseRepository courseRepository,
    IClassRepository classRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateClassCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        if (!request.CourseIds.Any())
        {
            return Result.Failure<Guid>(ClassErrors.NoCourseAssociated);
        }

        var courses = await courseRepository.GetQueryable()
            .Where(c => request.CourseIds.Contains(c.Id))
            .ToListAsync(cancellationToken);
        if (courses.Count != request.CourseIds.Count)
        {
            var invalidIds = request.CourseIds.Except(courses.Select(c => c.Id));
            return Result.Failure<Guid>(ClassErrors.InvalidCoursesAssociated(invalidIds));
        }

        var isExists = await classRepository.ExistsAsync(x => x.Title == request.Name, cancellationToken);
        if (isExists)
        {
            return Result.Failure<Guid>(ClassErrors.ClassAlreadyExists);
        }

        var @class = Class.Create(request.Name, userContext.UserId, request.CourseIds, request.Description);

        classRepository.Add(@class);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(@class.Id);
    }
}
