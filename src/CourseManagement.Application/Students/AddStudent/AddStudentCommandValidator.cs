using FluentValidation;

namespace CourseManagement.Application.Students.AddStudent;

internal sealed class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
{
    public AddStudentCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithErrorCode(StudentValidatorErrorCodes.EmptyFirstName)
            .MaximumLength(100).WithErrorCode(StudentValidatorErrorCodes.FirstNameMaxLengthExceeds);
        RuleFor(c => c.LastName)
            .NotEmpty().WithErrorCode(StudentValidatorErrorCodes.EmptyLastName)
            .MaximumLength(100).WithErrorCode(StudentValidatorErrorCodes.LastNameMaxLengthExceeds);
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(StudentValidatorErrorCodes.EmptyEmail)
            .EmailAddress().WithErrorCode(StudentValidatorErrorCodes.InvalidEmail);
        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(StudentValidatorErrorCodes.EmptyPassword)
            .MaximumLength(12).WithErrorCode(StudentValidatorErrorCodes.PasswordMaxLengthExceeds);
    }
}
