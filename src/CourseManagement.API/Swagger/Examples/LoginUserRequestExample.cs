using CourseManagement.API.Controllers.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace CourseManagement.API.Swagger.Examples;

internal sealed class LoginUserRequestExample : IExamplesProvider<LogInUserRequest>
{
    private static readonly LogInUserRequest Example = new(
        "staff@university.com",
        "Staff12345"
    );

    public LogInUserRequest GetExamples() => Example;
}
