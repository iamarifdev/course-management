using CourseManagement.Application.Base;
using CourseManagement.Domain.StudentCourseClasses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Students.GetStudentClassEnrollment;

internal sealed class GetStudentClassEnrollmentQueryHandler(IStudentCourseClassRepository repository)
    : IQueryHandler<GetStudentClassEnrollmentQuery, StudentClassEnrollmentResponse>
{
    public async Task<Result<StudentClassEnrollmentResponse>> Handle(
        GetStudentClassEnrollmentQuery request,
        CancellationToken cancellationToken
    )
    {
        var classEnrollment = await repository.GetQueryable()
            .Where(x => x.StudentId == request.StudentId && x.ClassId == request.ClassId)
            .Select(x => new StudentClassEnrollmentResponse(
                x.Id,
                new ClassResponse(x.ClassId, x.Class.Title),
                new StudentResponse(x.StudentId, x.Student.FirstName, x.Student.LastName),
                new StaffResponse(x.StaffId, x.EnrolledBy.FirstName, x.EnrolledBy.LastName),
                x.CreatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return classEnrollment is null
            ? Result.Failure<StudentClassEnrollmentResponse>(StudentErrors.NotEnrolledInClass)
            : Result.Success(classEnrollment);
    }
}
