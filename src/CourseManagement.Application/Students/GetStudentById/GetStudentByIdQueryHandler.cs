using CourseManagement.Application.Base;
using CourseManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Students.GetStudentById;

internal sealed class GetStudentByIdQueryHandler(IStudentRepository studentRepository)
    : IQueryHandler<GetStudentByIdQuery, StudentResponse>
{
    public async Task<Result<StudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetQueryable()
            .Where(x => x.Id == request.Id)
            .Select(x => new StudentResponse(
                x.Id,
                x.UserId,
                x.User.Email.Value,
                x.FirstName,
                x.LastName,
                x.StaffId
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return student == null
            ? Result.Failure<StudentResponse>(StudentErrors.StudentNotFound)
            : Result.Success(student);
    }
}
