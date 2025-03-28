using System.Net.Http.Json;
using CourseManagement.API.Controllers.Auth;
using CourseManagement.API.FunctionalTests.Users;
using CourseManagement.Application.Users.LoginUser;

namespace CourseManagement.API.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient = factory.CreateClient();

    protected async Task<string> GetAccessToken()
    {
        var loginResponse = await HttpClient.PostAsJsonAsync(
            "api/auth/login",
            new LogInUserRequest(
                UserData.LoginTestUserRequest.Email,
                UserData.LoginTestUserRequest.Password
            )
        );

        var response = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        return response!.AccessToken;
    }
}
