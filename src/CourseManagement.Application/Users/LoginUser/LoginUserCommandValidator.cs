using FluentValidation;

namespace CourseManagement.Application.Users.LoginUser;

internal sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithErrorCode(UserErrorCodes.LoginUser.EmptyEmail)
            .EmailAddress().WithErrorCode(UserErrorCodes.LoginUser.InvalidEmail);
        RuleFor(c => c.Password)
            .NotEmpty().WithErrorCode(UserErrorCodes.LoginUser.EmptyPassword);
    }
}
