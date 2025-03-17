using FluentValidation;

namespace CourseManagement.Application.Courses.CreateCourse;

internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithErrorCode(CourseErrorCodes.CreateCourse.EmptyName)
            .Matches("^[a-zA-Z0-9 ]*$").WithErrorCode(CourseErrorCodes.CreateCourse.InvalidName)
            .MaximumLength(100).WithErrorCode(CourseErrorCodes.CreateCourse.NameMaxLengthExceeds);
        RuleFor(c => c.Description)
            .MaximumLength(250).WithErrorCode(CourseErrorCodes.CreateCourse.DescriptionMaxLengthExceeds);
    }
}
