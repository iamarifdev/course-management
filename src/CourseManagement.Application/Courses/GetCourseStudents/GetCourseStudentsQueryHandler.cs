using CourseManagement.Application.Base;
using CourseManagement.Application.Students;
using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Courses.GetCourseStudents;

internal sealed class GetCourseStudentsQueryHandler(ICourseRepository repository)
    : IQueryHandler<GetCourseStudentsQuery, CourseStudentsResponse>
{
    public async Task<Result<CourseStudentsResponse>> Handle(
        GetCourseStudentsQuery request,
        CancellationToken cancellationToken
    )
    {
        var courseStudents = await repository.GetQueryable()
            .Where(x => x.Id == request.Id)
            .Select(s => new CourseStudentsResponse(
                s.Id,
                s.Title,
                s.Description,
                s.EnrolledStudentCourses.Select(c => new StudentResponse(
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

        return courseStudents is null
            ? Result.Failure<CourseStudentsResponse>(CourseErrors.NotFound)
            : Result.Success(courseStudents);
    }
}
