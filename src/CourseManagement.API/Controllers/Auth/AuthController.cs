using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Users.GetLoggedInUser;
using CourseManagement.Application.Users.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseManagement.API.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login user with email and password",
        Description = "Returns a pair of AccessToken and RefreshToken"
    )]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LogInUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("me")]
    [SwaggerOperation(
        Summary = "Get logged in User details",
        Description = "Returns the logged in User details"
    )]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLoggedInUser(IUserContext userContext, CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery(userContext.UserId);

        var result = await sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
