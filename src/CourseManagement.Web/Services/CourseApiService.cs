using Blazored.LocalStorage;
using System.Web;
using CourseManagement.Web.Models; // For HttpUtility

namespace CourseManagement.Web.Services;

public interface ICourseApiService
{
    Task<PaginatedResult<CourseResponse>?> GetCoursesAsync(string? filterText = null, int? pageNumber = 1,
        int? pageSize = 20);
}

public class CourseApiService(HttpClient httpClient, ILocalStorageService localStorage)
    : ApiService(httpClient, localStorage), ICourseApiService
{
    public async Task<PaginatedResult<CourseResponse>?> GetCoursesAsync(string? filterText = null, int? pageNumber = 1,
        int? pageSize = 20)
    {
        // Construct the query string
        var query = HttpUtility.ParseQueryString(string.Empty);
        if (!string.IsNullOrWhiteSpace(filterText))
        {
            query["filterText"] = filterText;
        }

        if (pageNumber.HasValue)
        {
            query["pageNumber"] = $"{pageNumber.Value}";
        }

        if (pageSize.HasValue)
        {
            query["pageSize"] = $"{pageSize.Value}";
        }

        var endpoint = $"api/courses?{query}";

        // Assuming the API endpoint for getting courses is "api/courses"
        // And it accepts filterText, pageNumber, pageSize as query parameters
        // Adjust the endpoint if it's different
        return await GetAsync<PaginatedResult<CourseResponse>>(endpoint);
    }
}
