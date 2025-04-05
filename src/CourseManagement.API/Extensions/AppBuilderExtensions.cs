using System.Text.Json.Serialization;
using CourseManagement.API.Middlewares;
using CourseManagement.API.Swagger;
using CourseManagement.API.Swagger.Examples;
using CourseManagement.Application.Base;
using CourseManagement.Infrastructure.Authentication;
using CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CourseManagement.API.Extensions;

internal static class AppBuilderExtensions
{
    public static void AddSwaggerExplorer(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.DocumentFilter<PathPrefixFilter>(Constants.ApiRoutePrefix);
            c.SwaggerDoc("v1", new OpenApiInfo { Title = Constants.ApiSwaggerDocTitle, Version = "v1" });
            c.ExampleFilters();
            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                }
            );
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Name = "Bearer",
                    },
                    new List<string>()
                }
            });
        });
        builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginUserRequestExample>();
    }

    public static void UseSwaggerExplorer(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(ui =>
        {
            var jsInterceptor = @"
            function (res) {
                if (!res.url.endsWith('/auth/login')) return res;
                if (res.status !== 200) return res;

                const content = res.body;

                if (content && content.accessToken) {
                    setTimeout(() => {
                        const authDefinitions = ui.authSelectors.definitionsToAuthorize();
                        const payload = {
                            Bearer: {
                                name: 'Bearer',
                                value: content.accessToken,
                                schema: authDefinitions.get(0).get('Bearer'),
                            },
                        };
                        ui.authActions.authorizeWithPersistOption(payload);
                    });
                }

                return res;
            }".Replace("\r\n", "").Replace("\n", "");

            ui.UseResponseInterceptor(jsInterceptor);
            ui.EnablePersistAuthorization();
        });
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        var logger = app.Services
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("Database");

        logger.LogInformation("Applying database migrations...");

        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();

        logger.LogInformation("Database migrations applied.");

        if (app.IsTestEnvironment())
        {
            return;
        }

        logger.LogInformation("Seeding database...");

        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        dbContext.Seed(passwordHasher);

        logger.LogInformation("Database seeded.");
    }

    public static void AddCors(this WebApplicationBuilder builder)
    {
        var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>() ??
                        throw new InvalidOperationException("Jwt config is not found");

        builder.Services.AddCors(options => options.AddPolicy(
            Constants.CorsPolicyName,
            p => p.WithOrigins(jwtConfig.Audience).AllowAnyHeader().AllowAnyMethod()
        ));
    }

    public static void ConfigureRoute(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }

    public static void AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    public static void UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static void UseRequestContextLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
    }

    public static bool IsTestEnvironment(this WebApplication _)
    {
        var envValue = Environment.GetEnvironmentVariable("IS_TEST");
        return !string.IsNullOrWhiteSpace(envValue);
    }
}
