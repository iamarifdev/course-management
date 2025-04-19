using CourseManagement.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CourseManagement.Web;

public static class DependencyInjection
{
    public static void AddApiServices(this WebAssemblyHostBuilder builder)
    {
        AddApiService<IAuthApiService, AuthApiService>(builder);
        AddApiService<IDashboardApiService, DashboardApiService>(builder);
    }

    private static void AddApiService<TInterface, TImplementation>(WebAssemblyHostBuilder builder)
        where TInterface : class where TImplementation : class, TInterface
    {
        builder.Services.AddHttpClient<TInterface, TImplementation>(client =>
        {
            var baseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8080";
            client.BaseAddress = new Uri(baseUrl);
        });
    }
}
