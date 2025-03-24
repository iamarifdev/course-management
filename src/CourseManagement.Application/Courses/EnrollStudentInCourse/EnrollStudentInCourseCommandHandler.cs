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
    IStudentCourseClassRepository studentCourseClassRepository) : ICommandHandler<EnrollStudentInCourseCommand>
{
    public async Task<Result> Handle(EnrollStudentInCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetQueryable()
            .Where(x => x.Id == request.CourseId)
            .Select(x => new
            {
                Course = x,
                IsStudentEnrolled = x.EnrolledStudentCourses.Any(
                    e => e.StudentId == request.StudentId
                ),
                ClassIds = x.CourseClasses.Select(cc => cc.ClassId).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (course is null)
        {
            return Result.Failure(CourseErrors.CourseNotFound);
        }

        if (course.IsStudentEnrolled)
        {
            return Result.Failure(CourseErrors.StudentAlreadyEnrolled);
        }

        var studentExists = await studentRepository.ExistsAsync(s => s.Id == request.StudentId, cancellationToken);
        if (!studentExists)
        {
            return Result.Failure(StudentErrors.StudentNotFound);
        }

        var studentCourse = StudentCourse.Create(request.StudentId, request.CourseId, request.StaffId);
        studentCourseRepository.Add(studentCourse);

        if (course.ClassIds.Any())
        {
            var existingClassEnrollments = await studentCourseClassRepository.GetQueryable()
                .Where(x => x.StudentId == request.StudentId && course.ClassIds.Contains(x.ClassId))
                .Select(x => x.ClassId)
                .ToListAsync(cancellationToken);

            var newClassEnrollments = course.ClassIds
                .Except(existingClassEnrollments)
                .Select(classId => StudentCourseClass.Create(
                    request.StudentId,
                    request.CourseId,
                    classId,
                    request.StaffId
                ))
                .ToList();

            if (newClassEnrollments.Any())
            {
                studentCourseClassRepository.AddRange(newClassEnrollments);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
