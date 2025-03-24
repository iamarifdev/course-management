using CourseManagement.Application.Base;
using CourseManagement.Domain.StudentCourseClasses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Students.GetStudentCourseClasses;

internal sealed class GetStudentClassesQueryHandler(IStudentCourseClassRepository repository)
    : IQueryHandler<GetStudentCourseClassesQuery, List<StudentCourseClassResponse>>
{
    public async Task<Result<List<StudentCourseClassResponse>>> Handle(GetStudentCourseClassesQuery request,
        CancellationToken cancellationToken)
    {
        var courseClasses = await repository.GetQueryable()
            .Where(x => x.StudentId == request.StudentId)
            .GroupBy(
                k => new { k.CourseId, k.Course.Title, k.Course.Description },
                e => new { e.Class.Id, e.Class.Title, e.Class.Description, e.CreatedAt }
            )
            .Select(x => new StudentCourseClassResponse(
                request.StudentId,
                new StudentCourseResponse(
                    x.Key.CourseId,
                    x.Key.Title,
                    x.Key.Description
                ),
                x.Select(s => new StudentClassResponse(
                    s.Id,
                    s.Title,
                    s.Description,
                    s.CreatedAt
                )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(courseClasses);
    }
}
