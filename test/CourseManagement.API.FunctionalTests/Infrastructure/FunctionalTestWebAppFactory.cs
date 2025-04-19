using CourseManagement.API.FunctionalTests.Auth;
using CourseManagement.API.FunctionalTests.Courses;
using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;
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

    private Guid StaffId { get; set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; set; }

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

        await InitializeTestUsersAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    private async Task CreateStaffUser()
    {
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        var user = User.Create(
            new Email(UserData.Staff.Email),
            Role.Staff,
            new Password(passwordHasher.Hash(UserData.Staff.Password).Value)
        );
        dbContext.Users.Add(user);

        var staff = Staff.Create(
            user.Id,
            UserData.Staff.FirstName,
            UserData.Staff.LastName,
            null,
            UserData.Staff.Department
        );
        dbContext.Staffs.Add(staff);

        await dbContext.SaveChangesAsync();

        StaffId = staff.Id;
    }

    private async Task CreateStudentUser()
    {
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        var user = User.Create(
            new Email(UserData.Student.Email),
            Role.Student,
            new Password(passwordHasher.Hash(UserData.Student.Password).Value)
        );
        dbContext.Users.Add(user);

        var student = Student.Create(
            user.Id,
            UserData.Student.FirstName,
            UserData.Student.LastName,
            StaffId
        );
        dbContext.Students.Add(student);

        await dbContext.SaveChangesAsync();

        StudentId = student.Id;
    }

    private async Task CreateCourse()
    {
        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var course = Course.Create(
            CourseData.Title,
            CourseData.Description,
            StaffId
        );
        dbContext.Courses.Add(course);

        await dbContext.SaveChangesAsync();

        CourseId = course.Id;
    }

    private async Task InitializeTestUsersAsync()
    {
        await CreateStaffUser();
        await CreateStudentUser();
        await CreateCourse();
    }
}
