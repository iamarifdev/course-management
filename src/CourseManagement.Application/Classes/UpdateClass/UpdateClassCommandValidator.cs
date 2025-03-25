using CourseManagement.Application.Base;
using FluentValidation;

namespace CourseManagement.Application.Classes.UpdateClass;

internal sealed class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
{
    public UpdateClassCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyId)
            .NotEqual(Guid.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyId);
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(ClassValidatorErrorCodes.EmptyName)
            .Matches(Constants.AlphaNumericRegex).WithErrorCode(ClassValidatorErrorCodes.InvalidName)
            .MaximumLength(100).WithErrorCode(ClassValidatorErrorCodes.NameMaxLengthExceeds);
        RuleFor(c => c.Description)
            .NotEqual(string.Empty).WithErrorCode(ClassValidatorErrorCodes.EmptyDescription)
            .MaximumLength(250).WithErrorCode(ClassValidatorErrorCodes.DescriptionMaxLengthExceeds);
    }
}
