using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CourseManagement.API.FunctionalTests.Infrastructure;
using CourseManagement.Application.Users.GetLoggedInUser;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CourseManagement.API.FunctionalTests.Auth;

public class GetLoggedInUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Get_ShouldReturnUnauthorized_WhenAccessTokenIsMissing()
    {
        var response = await HttpClient.GetAsync("api/auth/me");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_ShouldReturnUser_WhenAccessTokenIsNotMissing()
    {
        var accessToken = await GetAccessToken();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            accessToken);

        var user = await HttpClient.GetFromJsonAsync<UserResponse>("api/auth/me");

        user.Should().NotBeNull();
    }
}
