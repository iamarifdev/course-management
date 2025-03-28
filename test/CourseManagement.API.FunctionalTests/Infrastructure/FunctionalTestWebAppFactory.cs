using CourseManagement.API.FunctionalTests.Users;
using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;
using CourseManagement.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace CourseManagement.API.FunctionalTests.Infrastructure;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("course_management_test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("IS_TEST", "true");

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention()
            );
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        await InitializeTestUserAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    private async Task CreateStaffUser(
        string email,
        string password,
        string? firstName,
        string? lastName,
        string department
    )
    {
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        var user = User.Create(
            new Email(email),
            Role.Staff,
            new Password(passwordHasher.Hash(password).Value)
        );
        dbContext.Users.Add(user);

        var staff = Staff.Create(
            user.Id,
            firstName,
            lastName,
            null,
            department
        );
        dbContext.Staffs.Add(staff);

        await dbContext.SaveChangesAsync();
    }

    private async Task InitializeTestUserAsync()
    {
        await CreateStaffUser(
            UserData.Email,
            UserData.Password,
            UserData.FirstName,
            UserData.LastName,
            UserData.Department
        );
    }
}
