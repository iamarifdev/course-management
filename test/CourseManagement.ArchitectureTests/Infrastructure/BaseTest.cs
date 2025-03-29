using System.Reflection;
using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Infrastructure.Database;

namespace CourseManagement.ArchitectureTests.Infrastructure;

public class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
