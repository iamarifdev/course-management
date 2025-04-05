using CourseManagement.Application.Base;
using CourseManagement.Application.Students;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.StudentCourseClasses;
using CourseManagement.Domain.StudentCourses;
using CourseManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Courses.EnrollStudentInCourse;

internal sealed class EnrollStudentInCourseCommandHandler(
    IUnitOfWork unitOfWork,
    ICourseRepository courseRepository,
    IStudentRepository studentRepository,
    IStudentCourseRepository studentCourseRepository,
    IStudentCourseClassRepository studentCourseClassRepository
) : ICommandHandler<EnrollStudentInCourseCommand>
{
    public async Task<Result> Handle(EnrollStudentInCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await courseRepository.GetQueryable()
            .Where(x => x.Id == request.CourseId)
            .Select(x => new
            {
                Course = x,
                IsStudentEnrolled = x.EnrolledStudentCourses.Any(
                    e => e.StudentId == request.StudentId
                ),
                // Only get class ids that are not enrolled by the student
                ClassIds = x.CourseClasses
                    .Where(cc => !x.EnrolledStudentClasses.Any(
                        scc => scc.StudentId == request.StudentId && scc.ClassId == cc.ClassId
                    ))
                    .Select(cc => cc.ClassId)
                    .ToList(),
                StudentExists = studentRepository.GetQueryable().Any(s => s.Id == request.StudentId)
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return Result.Failure(CourseErrors.NotFound);
        }

        if (!result.StudentExists)
        {
            return Result.Failure(StudentErrors.NotFound);
        }

        if (result.IsStudentEnrolled)
        {
            return Result.Failure(CourseErrors.StudentAlreadyEnrolled);
        }

        var studentCourse = StudentCourse.Create(request.StudentId, request.CourseId, request.StaffId);
        studentCourseRepository.Add(studentCourse);

        if (result.ClassIds.Any())
        {
            var newClassEnrollments = result.ClassIds
                .Select(classId => StudentCourseClass.Create(
                    request.StudentId,
                    request.CourseId,
                    classId,
                    request.StaffId
                ))
                .ToList();

            studentCourseClassRepository.AddRange(newClassEnrollments);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
