using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(
    IUserService userService,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
    : ICommandHandler<LoginUserCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserInfoAsync(x => x.Email == new Email(request.Email), cancellationToken);
        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.UserNotFoundByEmail);
        }

        var result = passwordHasher.Verify(request.Password, user.PasswordHash);
        if (result.IsFailure)
        {
            return Result.Failure<LoginResponse>(result.Error);
        }


        var accessToken = tokenService.GenerateAccessToken(user.Id, user.UserId, user.Email, user.Role);
        var refreshToken = tokenService.GenerateRefreshToken();

        return new LoginResponse(accessToken, refreshToken);
    }
}
