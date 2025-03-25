using CourseManagement.Application.Base;
using CourseManagement.Application.Students;
using CourseManagement.Domain.Classes;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Classes.GetClassStudents;

internal sealed class GetClassStudentsQueryHandler(IClassRepository repository)
    : IQueryHandler<GetClassStudentsQuery, ClassStudentsResponse>
{
    public async Task<Result<ClassStudentsResponse>> Handle(GetClassStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var classStudents = await repository.GetQueryable()
            .Where(x => x.Id == request.Id)
            .Select(s => new ClassStudentsResponse(
                s.Id,
                s.Title,
                s.Description,
                s.EnrolledStudentClasses.Select(c => new StudentResponse(
                    c.Student.Id,
                    c.Student.UserId,
                    c.Student.User.Email.Value,
                    c.Student.FirstName,
                    c.Student.LastName,
                    c.Student.StaffId,
                    c.Student.CreatedAt
                )).ToList(),
                s.CreatedAt,
                s.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return classStudents is null
            ? Result.Failure<ClassStudentsResponse>(ClassErrors.NotFound)
            : Result.Success(classStudents);
    }
}
