using CourseManagement.Application.Base;
using CourseManagement.Application.Students;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.StudentCourseClasses;
using CourseManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Classes.EnrollStudentInClass;

internal sealed class EnrollStudentInClassCommandHandler(
    IUnitOfWork unitOfWork,
    IClassRepository classRepository,
    IStudentRepository studentRepository,
    IStudentCourseClassRepository studentCourseClassRepository) : ICommandHandler<EnrollStudentInClassCommand>
{
    public async Task<Result> Handle(EnrollStudentInClassCommand request, CancellationToken cancellationToken)
    {
        var @class = await classRepository.GetQueryable()
            .Where(x => x.Id == request.ClassId)
            .Select(x => new
            {
                CourseId = x.CourseClasses
                    .Where(e => e.ClassId == request.ClassId)
                    .Select(s => s.CourseId)
                    .FirstOrDefault(),
                IsStudentEnrolled = x.EnrolledStudentClasses.Any(
                    e => e.StudentId == request.StudentId
                ),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (@class is null)
        {
            return Result.Failure(ClassErrors.ClassNotFound);
        }

        if (@class.IsStudentEnrolled)
        {
            return Result.Failure(ClassErrors.StudentAlreadyEnrolled);
        }

        var studentExists = await studentRepository.ExistsAsync(s => s.Id == request.StudentId, cancellationToken);
        if (!studentExists)
        {
            return Result.Failure(StudentErrors.StudentNotFound);
        }
        
        var studentCourseClass = StudentCourseClass.Create(
            request.StudentId,
            @class.CourseId,
            request.ClassId,
            request.StaffId
        );
        studentCourseClassRepository.Add(studentCourseClass);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
