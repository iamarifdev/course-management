using CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.API;

public static class BuilderServicesExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
