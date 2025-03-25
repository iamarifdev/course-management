using FluentValidation;

namespace CourseManagement.Application.Students.UpdateStudent;

internal sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(StudentValidatorErrorCodes.EmptyId)
            .NotEqual(Guid.Empty).WithErrorCode(StudentValidatorErrorCodes.EmptyId);
        RuleFor(c => c.FirstName)
            .NotEqual(string.Empty).WithErrorCode(StudentValidatorErrorCodes.EmptyFirstName)
            .MaximumLength(100).WithErrorCode(StudentValidatorErrorCodes.FirstNameMaxLengthExceeds);
        RuleFor(c => c.LastName)
            .NotEqual(string.Empty).WithErrorCode(StudentValidatorErrorCodes.EmptyLastName)
            .MaximumLength(100).WithErrorCode(StudentValidatorErrorCodes.LastNameMaxLengthExceeds);
        RuleFor(c => c.Email)
            .NotEqual(string.Empty).WithErrorCode(StudentValidatorErrorCodes.EmptyEmail)
            .EmailAddress().WithErrorCode(StudentValidatorErrorCodes.InvalidEmail);
        RuleFor(c => c.Password)
            .NotEqual(string.Empty).WithErrorCode(StudentValidatorErrorCodes.EmptyPassword)
            .MaximumLength(12).WithErrorCode(StudentValidatorErrorCodes.PasswordMaxLengthExceeds);
    }
}
