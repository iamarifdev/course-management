using FluentValidation;

namespace CourseManagement.Application.Classes.EnrollStudentInClass;

internal sealed class EnrollStudentInClassCommandValidator : AbstractValidator<EnrollStudentInClassCommand>
{
    public EnrollStudentInClassCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyStudentId);
        RuleFor(x => x.ClassId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyClassId);
        RuleFor(x => x.StaffId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyStaffId);
    }
}
