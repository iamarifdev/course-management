using Blazored.LocalStorage;
using CourseManagement.Web.Models;

namespace CourseManagement.Web.Services;

public interface IDashboardApiService
{
    Task<DashboardSummary?> GetDashboardSummary();
}

public class DashboardApiService(HttpClient httpClient, ILocalStorageService localStorage)
    : ApiService(httpClient, localStorage), IDashboardApiService
{
    public async Task<DashboardSummary?> GetDashboardSummary()
    {
        return await GetAsync<DashboardSummary>("api/dashboard/summary");
    }
}
