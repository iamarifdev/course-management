using CourseManagement.API;
using CourseManagement.API.Extensions;
using CourseManagement.Application;
using CourseManagement.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration)
);

builder.AddCors();

builder.ConfigureRoute();

builder.Services.AddControllers();

builder.AddSwaggerExplorer();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExplorer();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UsePathBase(Constants.ApiRoutePrefix);
app.UseRouting();
app.UseCors(Constants.CorsPolicyName);

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
