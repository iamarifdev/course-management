using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.EnrollStudentInCourse;

public record EnrollStudentInCourseCommand(Guid CourseId, Guid StudentId, Guid StaffId) : ICommand;

// internal sealed class EnrollStudentInCourseCommandHandler(
//     IUnitOfWork unitOfWork,
//     ICourseRepository courseRepository,
//     IStudentRepository studentRepository,
//     IStudentCourseRepository studentCourseRepository,
//     IStudentCourseClassRepository studentCourseClassRepository) : ICommandHandler<EnrollStudentInCourseCommand>
// {
//     public async Task<Result> Handle(EnrollStudentInCourseCommand request, CancellationToken cancellationToken)
//     {
//         var course = await courseRepository.GetQueryable()
//             .Include(x => x.EnrolledStudentCourses.Where(e => e.StudentId == request.StudentId))
//             .Include(x => x.CourseClasses)
//             .FirstOrDefaultAsync(x => x.Id == request.CourseId, cancellationToken);
//         if (course is null)
//         {
//             return Result.Failure(CourseErrors.CourseNotFound);
//         }
//
//         var isExists = await studentRepository.ExistsAsync(x => x.Id == request.StudentId, cancellationToken);
//         if (!isExists)
//         {
//             return Result.Failure(StudentErrors.StudentNotFound);
//         }
//
//         if (course.EnrolledStudentCourses.Any())
//         {
//             return Result.Failure(CourseErrors.StudentAlreadyEnrolled);
//         }
//
//         var studentCourse = StudentCourse.Create(request.StudentId, request.CourseId, request.StaffId);
//         studentCourseRepository.Add(studentCourse);
//
//         var classIds = course.CourseClasses.Select(x => x.ClassId).ToList();
//         var enrolledClassIds = await studentCourseClassRepository.GetQueryable()
//             .Where(x => x.StudentId == request.StudentId && classIds.Contains(x.ClassId))
//             .Select(x => x.ClassId)
//             .ToListAsync(cancellationToken);
//         var availableClassIds = classIds.Except(enrolledClassIds).ToList();
//         if (!availableClassIds.Any())
//         {
//             // This means, classes are individually enrolled, so we can just enroll the student in the course
//             await unitOfWork.SaveChangesAsync(cancellationToken);
//             
//             return Result.Success();
//         }
//         
//         var studentCourseClass = course.CourseClasses.Select(x => StudentCourseClass.Create(
//             request.StudentId,
//             request.CourseId,
//             x.ClassId,
//             request.StaffId
//         )).ToList();
//         studentCourseClassRepository.AddRange(studentCourseClass);
//         
//         await unitOfWork.SaveChangesAsync(cancellationToken);
//         
//         return Result.Success();
//     }
// }
