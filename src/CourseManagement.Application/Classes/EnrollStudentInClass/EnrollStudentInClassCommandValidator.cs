using FluentValidation;

namespace CourseManagement.Application.Classes.EnrollStudentInClass;

internal sealed class EnrollStudentInClassCommandValidator : AbstractValidator<EnrollStudentInClassCommand>
{
    public EnrollStudentInClassCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyStudentId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyStudentId);
        RuleFor(x => x.ClassId)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyClassId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyClassId);
        RuleFor(x => x.StaffId)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyStaffId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyStaffId);
    }
}
