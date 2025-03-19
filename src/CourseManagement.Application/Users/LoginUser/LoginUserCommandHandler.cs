using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(
    IUserRepository userRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
    : ICommandHandler<LoginUserCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);

        var user = await userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.UserNotFoundByEmail);
        }

        var result = passwordHasher.Verify(request.Password, user.Password.Value);
        if (result.IsFailure)
        {
            return Result.Failure<LoginResponse>(result.Error);
        }

        var accessToken = tokenService.GenerateAccessToken(user.Id, user.Email, user.Role);
        var refreshToken = tokenService.GenerateRefreshToken();

        return new LoginResponse(accessToken, refreshToken);
    }
}