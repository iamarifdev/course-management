using CourseManagement.API.Extensions;
using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Application.Courses;
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
using Swashbuckle.AspNetCore.Annotations;

namespace CourseManagement.API.Controllers.Courses;

[Authorize(Roles = Roles.Staff)]
[ApiController]
[Route("[controller]")]
public class CoursesController(ISender sender, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Courses by pagination and filtering",
        Description = "Returns a list of paginated Courses"
    )]
    [ProducesResponseType(typeof(PaginatedResult<CourseResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCourses(
        [FromQuery] GetAllCoursesRequest request,
        CancellationToken cancellationToken
    )
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
    [SwaggerOperation(
        Summary = "Get all Classes in a Course",
        Description = "Returns a list of associated Classes with the Course"
    )]
    [ProducesResponseType(typeof(CourseClassesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourseClassesById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseClassesQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}/students")]
    [SwaggerOperation(
        Summary = "Get all enrolled Students in a Course",
        Description = "Returns a Course detail with list of enrolled students in the Course"
    )]
    [ProducesResponseType(typeof(CourseStudentsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourseStudentsById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseStudentsQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost("{id:guid}/students")]
    [SwaggerOperation(Summary = "Enroll a Student in a Course and it's all associated Classes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> EnrollStudentInCourse(
        Guid id,
        EnrollStudentInCourseRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new EnrollStudentInCourseCommand(id, Guid.Parse(request.StudentId), userContext.StaffId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }

    [HttpGet("{id:guid}", Name = nameof(GetCourseById))]
    [SwaggerOperation(
        Summary = "Get a Course by ID",
        Description = "Returns a Course with the specified ID"
    )]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourseById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Course",
        Description = "Returns the newly created Course"
    )]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(request.Title, request.Description, userContext.StaffId).Sanitize();

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute(nameof(GetCourseById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update a Course by ID",
        Description = "Returns the updated Course"
    )]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCourseById(
        Guid id,
        UpdateCourseRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateCourseCommand(id, request.Title, request.Description).Sanitize();

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a Course by ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCourseById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
