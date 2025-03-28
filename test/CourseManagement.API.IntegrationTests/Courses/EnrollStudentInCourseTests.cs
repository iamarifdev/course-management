using CourseManagement.API.IntegrationTests.Infrastructure;
using CourseManagement.Application.Courses;
using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Application.Courses.EnrollStudentInCourse;
using CourseManagement.Application.Students;
using FluentAssertions;

namespace CourseManagement.API.IntegrationTests.Courses;

public class EnrollStudentInCourseTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IntegrationTestWebAppFactory _factory = factory;

    [Fact]
    public async Task EnrollStudentInCourse_ShouldReturnSuccess_Response()
    {
        // Arrange
        var courseCommand = new CreateCourseCommand("Course 1", "Description", _factory.StaffId);
        var courseResult = await Sender.Send(courseCommand);

        var command = new EnrollStudentInCourseCommand(
            courseResult.Value.Id,
            _factory.StudentId,
            _factory.StaffId
        );
        
        // Act
        var result = await Sender.Send(command);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task EnrollStudentInCourse_ShouldReturnFailure_WhenCourseNotFound()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var command = new EnrollStudentInCourseCommand(
            courseId,
            _factory.StudentId,
            _factory.StaffId
        );
        
        // Act
        var result = await Sender.Send(command);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CourseErrors.NotFound);
    }
    
    [Fact]
    public async Task EnrollStudentInCourse_ShouldReturnFailure_WhenStudentAlreadyEnrolled()
    {
        // Arrange
        var courseCommand = new CreateCourseCommand("Course 2", "Description", _factory.StaffId);
        var courseResult = await Sender.Send(courseCommand);

        var command = new EnrollStudentInCourseCommand(
            courseResult.Value.Id,
            _factory.StudentId,
            _factory.StaffId
        );
        
        // Act
        var successResult = await Sender.Send(command);
        var errorResult = await Sender.Send(command);
        
        // Assert
        successResult.IsSuccess.Should().BeTrue();
        errorResult.IsFailure.Should().BeTrue();
        errorResult.Error.Should().Be(CourseErrors.StudentAlreadyEnrolled);
    }
    
    [Fact]
    public async Task EnrollStudentInCourse_ShouldReturnFailure_WhenStudentNotFound()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var courseCommand = new CreateCourseCommand("Course 3", "Description", _factory.StaffId);
        var courseResult = await Sender.Send(courseCommand);

        var command = new EnrollStudentInCourseCommand(
            courseResult.Value.Id,
            studentId,
            _factory.StaffId
        );
        
        // Act
        var result = await Sender.Send(command);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(StudentErrors.NotFound);
    }
}
