using System.Net;
using System.Net.Http.Json;
using CourseManagement.API.FunctionalTests.Infrastructure;
using FluentAssertions;

namespace CourseManagement.API.FunctionalTests.Auth;

public class LoginUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserDoesNotExist()
    {
        var request = UserData.InvalidLoginTestUserRequest;

        var response = await HttpClient.PostAsJsonAsync("api/auth/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenUserExists()
    {
        var request = UserData.LoginTestUserRequest;

        var response = await HttpClient.PostAsJsonAsync("api/auth/login", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
