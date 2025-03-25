using CourseManagement.Application.Base;
using FluentValidation;

namespace CourseManagement.Application.Courses.UpdateCourse;

internal sealed class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(CourseValidatorErrorCodes.EmptyId)
            .NotEqual(Guid.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyId);
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(CourseValidatorErrorCodes.EmptyName)
            .Matches(Constants.AlphaNumericRegex).WithErrorCode(CourseValidatorErrorCodes.InvalidName)
            .MaximumLength(100).WithErrorCode(CourseValidatorErrorCodes.NameMaxLengthExceeds);
        RuleFor(c => c.Description)
            .NotEqual(string.Empty).WithErrorCode(CourseValidatorErrorCodes.EmptyDescription)
            .MaximumLength(250).WithErrorCode(CourseValidatorErrorCodes.DescriptionMaxLengthExceeds);
    }
}
