using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using CourseManagement.Web.Models;

namespace CourseManagement.Web.Services;

public interface IApiService
{
    Task<LoginResponse?> Login(LoginRequest loginRequest);
    Task<DashboardSummary?> GetDashboardSummary();
    Task Logout();
}

public class ApiService(HttpClient httpClient, ILocalStorageService localStorage) : IApiService
{
    private const string TokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    private async Task SetAuthHeader()
    {
        var token = await localStorage.GetItemAsStringAsync(TokenKey);
        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        if (!endpoint.Contains("/auth/login"))
        {
            await SetAuthHeader();
        }

        var response = await httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    private async Task<T?> PostAsJsonAsync<T>(string endpoint, object data)
    {
        if (!endpoint.Contains("/auth/login"))
        {
            await SetAuthHeader();
        }

        var response = await httpClient.PostAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    // Specific API methods
    
    public async Task<LoginResponse?> Login(LoginRequest loginRequest)
    {
        var response = await PostAsJsonAsync<LoginResponse>("api/auth/login", loginRequest);
        if (response == null || string.IsNullOrEmpty(response.AccessToken))
        {
            // log it
            return null;
        }

        await localStorage.SetItemAsStringAsync(TokenKey, response.AccessToken);
        await localStorage.SetItemAsStringAsync(RefreshTokenKey, response.RefreshToken);
        
        return response;
    }
    
    public async Task<DashboardSummary?> GetDashboardSummary()
    {
        return await GetAsync<DashboardSummary>("api/dashboard/summary");
    }
    
    public async Task Logout()
    {
        await localStorage.RemoveItemAsync(TokenKey);
        await localStorage.RemoveItemAsync(RefreshTokenKey);
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
