using FluentValidation;

namespace CourseManagement.Application.Courses.EnrollStudentInCourse;

internal sealed class EnrollStudentInCourseCommandValidator : AbstractValidator<EnrollStudentInCourseCommand>
{
    public EnrollStudentInCourseCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithErrorCode(CourseValidatorErrorCodes.EmptyStudentId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyStudentId);
        RuleFor(x => x.CourseId)
            .NotEmpty().WithErrorCode(CourseValidatorErrorCodes.EmptyCourseId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyCourseId);
        RuleFor(x => x.StaffId)
            .NotEmpty().WithErrorCode(CourseValidatorErrorCodes.EmptyStaffId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyStaffId);
    }
}
