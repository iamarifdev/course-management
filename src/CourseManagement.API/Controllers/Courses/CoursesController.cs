using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Application.Courses.DeleteCourse;
using CourseManagement.Application.Courses.EnrollStudentInCourse;
using CourseManagement.Application.Courses.GetCourseById;
using CourseManagement.Application.Courses.GetCourseClasses;
using CourseManagement.Application.Courses.GetCourses;
using CourseManagement.Application.Courses.GetCourseStudents;
using CourseManagement.Application.Courses.UpdateCourse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class CoursesController(ISender sender, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetAllCourses(
        [FromQuery] GetAllCoursesRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetCoursesQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            FilterText = request.FilterText,
        };

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}/classes")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetCourseClassesById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseClassesQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}/students")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetCourseStudentsById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseStudentsQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost("{id:guid}/students")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> EnrollStudentInCourse(
        Guid id,
        EnrollStudentInCourseRequest request,
        CancellationToken cancellationToken)
    {
        var query = new EnrollStudentInCourseCommand(id, Guid.Parse(request.StudentId), userContext.StaffId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }

    [HttpGet("{id:guid}", Name = nameof(GetCourseById))]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetCourseById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> CreateCourse(
        CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(request.Title, request.Description);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute(nameof(GetCourseById), new { id = result.Value }, null);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> UpdateCourseById(
        Guid id,
        UpdateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseCommand(id, request.Title, request.Description);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> DeleteCourseById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
