using CourseManagement.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagement.Application.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private IServiceScope Scope { get; }
    
    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Scope = factory.Services.CreateScope();

        Sender = Scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}
