using CourseManagement.Application.Base;
using FluentValidation;

namespace CourseManagement.Application.Classes.CreateClass;

internal sealed class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyName)
            .Matches(Constants.AlphaNumericRegex).WithErrorCode(ClassValidatorErrorCodes.InvalidName)
            .MaximumLength(100).WithErrorCode(ClassValidatorErrorCodes.NameMaxLengthExceeds);
        RuleFor(c => c.Description)
            .NotEqual(string.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyDescription)
            .MaximumLength(250).WithErrorCode(ClassValidatorErrorCodes.DescriptionMaxLengthExceeds);
        RuleFor(c => c.CourseIds)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyCourseIds);
        RuleFor(c => c.CreatedById)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyCreatedById)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyCreatedById);
    }
}
