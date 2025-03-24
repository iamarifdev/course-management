using CourseManagement.Application.Base;
using CourseManagement.Domain.StudentCourseClasses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Students.GetClassmates;

internal sealed class GetClassStudentNamesQueryHandler(IStudentCourseClassRepository repository)
    : IQueryHandler<GetClassmatesQuery, List<ClassmatesResponse>>
{
    public async Task<Result<List<ClassmatesResponse>>> Handle(GetClassmatesQuery request,
        CancellationToken cancellationToken)
    {
        var enrolledClassIds = await repository.GetQueryable()
            .Where(x => x.StudentId == request.StudentId)
            .Select(x => x.ClassId)
            .ToListAsync(cancellationToken);

        if (!enrolledClassIds.Any())
        {
            return Result.Failure<List<ClassmatesResponse>>(StudentErrors.NotEnrolledAnyClass);
        }

        var classmates = await repository.GetQueryable()
            .Where(x => enrolledClassIds.Contains(x.ClassId) && x.StudentId != request.StudentId)
            .Select(x => new ClassmatesResponse(
                x.StudentId,
                x.Student.FirstName,
                x.Student.LastName
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(classmates);
    }
}
