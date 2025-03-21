using CourseManagement.Application.Base;
using FluentValidation;

namespace CourseManagement.Application.Courses.CreateCourse;

internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(CourseValidatorErrorCodes.EmptyName)
            .Matches(Constants.AlphaNumericRegex).WithErrorCode(CourseValidatorErrorCodes.InvalidName)
            .MaximumLength(100).WithErrorCode(CourseValidatorErrorCodes.NameMaxLengthExceeds);
        RuleFor(c => c.Description)
            .MaximumLength(250).WithErrorCode(CourseValidatorErrorCodes.DescriptionMaxLengthExceeds);
    }
}
