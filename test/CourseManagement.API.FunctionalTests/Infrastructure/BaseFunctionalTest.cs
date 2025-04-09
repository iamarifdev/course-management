using System.Net.Http.Headers;
using System.Net.Http.Json;
using CourseManagement.API.Controllers.Auth;
using CourseManagement.API.FunctionalTests.Auth;
using CourseManagement.Application.Users.LoginUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CourseManagement.API.FunctionalTests.Infrastructure;

public abstract class BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    : IClassFixture<FunctionalTestWebAppFactory>
{
    protected readonly HttpClient HttpClient = factory.CreateClient();

    protected async Task<string> GetAccessToken(
        string? email = null,
        string? password = null
    )
    {
        email ??= UserData.Staff.Email;
        password ??= UserData.Staff.Password;
        
        var loginResponse = await HttpClient.PostAsJsonAsync(
            "api/auth/login",
            new LogInUserRequest(email, password)
        );

        var response = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        return response!.AccessToken;
    }

    protected async Task AuthenticateAsStaffAsync()
    {
        var accessToken = await GetAccessToken();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            accessToken);
    }
    

    protected async Task AuthenticateAsStudentAsync()
    {
        var accessToken = await GetAccessToken(UserData.Student.Email, UserData.Student.Password);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            accessToken);
    }
}
