using CourseManagement.API.FunctionalTests.Infrastructure;
using System.Net;
using System.Net.Http.Json;
using CourseManagement.API.Controllers.Classes;
using CourseManagement.Application.Base;
using CourseManagement.Application.Classes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.FunctionalTests.Classes;

public class CreateClassTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly FunctionalTestWebAppFactory _factory = factory;
    private const string ApiEndpoint = "/classes";
    
    [Fact]
    public async Task CreateClass_WithValidData_ShouldReturnCreatedClass()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: "TestClass101",
            CourseIds: [_factory.CourseId],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var classResponse = await response.Content.ReadFromJsonAsync<ClassResponse>();
        classResponse.Should().NotBeNull();
        classResponse!.Title.Should().Be(request.Title);
        classResponse.Description.Should().Be(request.Description);
    }

    [Fact]
    public async Task CreateClass_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: "",
            CourseIds: [Guid.NewGuid()],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        problemDetails.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateClass_WithInvalidName_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: "Invalid@Name#",
            CourseIds: [Guid.NewGuid()],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        // var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        // error.Should().NotBeNull();
        // error!.Errors.Should().ContainKey(ClassValidatorErrorCodes.InvalidName);
    }

    [Fact]
    public async Task CreateClass_WithNameExceedingMaxLength_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: new string('A', 101), // 101 characters, max is 100
            CourseIds: [Guid.NewGuid()],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        // var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        // error.Should().NotBeNull();
        // error!.Errors.Should().ContainKey(ClassValidatorErrorCodes.NameMaxLengthExceeds);
    }

    [Fact]
    public async Task CreateClass_WithEmptyDescription_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: "TestClass101",
            CourseIds: [Guid.NewGuid()],
            Description: ""
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        // var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        // error.Should().NotBeNull();
        // error!.Errors.Should().ContainKey(ClassValidatorErrorCodes.EmptyDescription);
    }

    [Fact]
    public async Task CreateClass_WithDescriptionExceedingMaxLength_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: "TestClass101",
            CourseIds: [Guid.NewGuid()],
            Description: new string('A', 251) // 251 characters, max is 250
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        // var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        // error.Should().NotBeNull();
        // error!.Errors.Should().ContainKey(ClassValidatorErrorCodes.DescriptionMaxLengthExceeds);
    }

    [Fact]
    public async Task CreateClass_WithEmptyCourseIds_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsStaffAsync();
        var request = new CreateClassRequest(
            Title: "TestClass101",
            CourseIds: [],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        // var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        // error.Should().NotBeNull();
        // error!.Errors.Should().ContainKey(ClassValidatorErrorCodes.EmptyCourseIds);
    }

    [Fact]
    public async Task CreateClass_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = new CreateClassRequest(
            Title: "TestClass101",
            CourseIds: [Guid.NewGuid()],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateClass_WithNonStaffRole_ShouldReturnForbidden()
    {
        // Arrange
        await AuthenticateAsStudentAsync(); // Assuming this method exists in BaseFunctionalTest
        var request = new CreateClassRequest(
            Title: "TestClass101",
            CourseIds: [Guid.NewGuid()],
            Description: "Test class description"
        );

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
