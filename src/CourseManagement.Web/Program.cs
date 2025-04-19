using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CourseManagement.Web;
using CourseManagement.Web.Services;

#pragma warning disable S1075
const string apiUrl = "http://localhost:8080";
#pragma warning restore S1075

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IApiService, ApiService>();

await builder.Build().RunAsync();
