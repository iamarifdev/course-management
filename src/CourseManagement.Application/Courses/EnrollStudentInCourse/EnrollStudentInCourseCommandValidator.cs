using FluentValidation;

namespace CourseManagement.Application.Courses.EnrollStudentInCourse;

internal sealed class EnrollStudentInCourseCommandValidator : AbstractValidator<EnrollStudentInCourseCommand>
{
    public EnrollStudentInCourseCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyStudentId);
        RuleFor(x => x.CourseId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyCourseId);
        RuleFor(x => x.StaffId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyStaffId);
    }
}
