using System.Reflection;
using CourseManagement.ArchitectureTests.Infrastructure;
using CourseManagement.Domain.Base;
using FluentAssertions;
using NetArchTest.Rules;

namespace CourseManagement.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void Entities_ShouldHave_PrivateParameterlessConstructor()
    {
        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes();

        var failingTypes = entityTypes
            .Select(entityType => new
            {
                entityType,
                constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
            })
            .Where(t => !t.constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            .Select(t => t.entityType).ToList();

        failingTypes.Should().BeEmpty();
    }
}
