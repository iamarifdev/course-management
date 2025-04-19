using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace CourseManagement.Web.Services;

public abstract class ApiService(HttpClient httpClient, ILocalStorageService localStorage)
{
    protected const string TokenKey = "access_token";
    protected const string RefreshTokenKey = "refresh_token";

    private async Task SetAuthHeader()
    {
        var token = await localStorage.GetItemAsStringAsync(TokenKey);
        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        if (!endpoint.Contains("/auth/login"))
        {
            await SetAuthHeader();
        }

        var response = await httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> PostAsJsonAsync<T>(string endpoint, object data)
    {
        if (!endpoint.Contains("/auth/login"))
        {
            await SetAuthHeader();
        }

        var response = await httpClient.PostAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}
