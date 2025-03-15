using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
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
        
        var verifyResult = Password.VerifyHash(request.Password, user.Password.Value);
        if (verifyResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(verifyResult.Error);
        }
        
        var accessToken = tokenService.GenerateAccessToken(user.Id, user.Email, user.Role);
        var refreshToken = tokenService.GenerateRefreshToken();
        
        return new LoginResponse(accessToken, refreshToken);
    }
}
