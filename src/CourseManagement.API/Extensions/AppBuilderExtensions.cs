using CourseManagement.API.Middlewares;
using CourseManagement.Application.Base;
using CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.API.Extensions;

internal static class AppBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("Database");
        
        logger.LogInformation("Applying database migrations...");
        
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        
        logger.LogInformation("Database migrations applied.");

        logger.LogInformation("Seeding database...");
        
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        dbContext.Seed(passwordHasher);
        
        logger.LogInformation("Database seeded.");
    }

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    
    public static void UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
    }
}
