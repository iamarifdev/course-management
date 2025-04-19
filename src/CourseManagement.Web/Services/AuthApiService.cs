using Blazored.LocalStorage;
using CourseManagement.Web.Models;

namespace CourseManagement.Web.Services;

public interface IAuthApiService
{
    Task<LoginResponse?> Login(LoginRequest loginRequest);
    Task Logout();
}

public class AuthApiService(HttpClient httpClient, ILocalStorageService localStorage)
    : ApiService(httpClient, localStorage), IAuthApiService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task<LoginResponse?> Login(LoginRequest loginRequest)
    {
        var response = await PostAsJsonAsync<LoginResponse>("api/auth/login", loginRequest);
        if (response == null || string.IsNullOrEmpty(response.AccessToken))
        {
            // log it
            return null;
        }

        await _localStorage.SetItemAsStringAsync(TokenKey, response.AccessToken);
        await _localStorage.SetItemAsStringAsync(RefreshTokenKey, response.RefreshToken);

        return response;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(TokenKey);
        await _localStorage.RemoveItemAsync(RefreshTokenKey);
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
