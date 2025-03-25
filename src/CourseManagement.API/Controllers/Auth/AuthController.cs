using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Users.GetLoggedInUser;
using CourseManagement.Application.Users.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
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
    public async Task<IActionResult> GetLoggedInUser(IUserContext userContext, CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery(userContext.UserId);

        var result = await sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
