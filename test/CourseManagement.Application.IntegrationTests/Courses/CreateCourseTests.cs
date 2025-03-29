using CourseManagement.Application.Courses;
using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Application.IntegrationTests.Infrastructure;
using FluentAssertions;

namespace CourseManagement.Application.IntegrationTests.Courses;

public class CreateCourseTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IntegrationTestWebAppFactory _factory = factory;

    [Fact]
    public async Task CreateCourse_ShouldReturnSuccess_Response()
    {
        // Arrange
        var command = new CreateCourseCommand("Course 1", "Description", _factory.StaffId);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.Title.Should().Be(command.Name);
        result.Value.Description.Should().Be(command.Description);
    }

    [Fact]
    public async Task CreateCourse_ShouldReturnFailure_WhenCourseAlreadyExists()
    {
        // Arrange
        var command = new CreateCourseCommand("Course 1", "Description", _factory.StaffId);
        
        // Act
        await Sender.Send(command);
        var result = await Sender.Send(command);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CourseErrors.AlreadyExists);
    }
}
