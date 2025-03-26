using CourseManagement.API.Extensions;
using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Application.Classes;
using CourseManagement.Application.Classes.CreateClass;
using CourseManagement.Application.Classes.DeleteClass;
using CourseManagement.Application.Classes.EnrollStudentInClass;
using CourseManagement.Application.Classes.GetClassById;
using CourseManagement.Application.Classes.GetClassCourses;
using CourseManagement.Application.Classes.GetClasses;
using CourseManagement.Application.Classes.GetClassStudents;
using CourseManagement.Application.Classes.UpdateClass;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseManagement.API.Controllers.Classes;

[Authorize(Roles = Roles.Staff)]
[ApiController]
[Route("[controller]")]
public class ClassesController(ISender sender, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all classes by pagination and filtering",
        Description = "Returns a list of paginated classes"
    )]
    [ProducesResponseType(typeof(PaginatedResult<ClassResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllClasses(
        [FromQuery] GetAllClassesRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetClassesQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            FilterText = request.FilterText,
        };

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}/courses")]
    [SwaggerOperation(
        Summary = "Get all courses in a class",
        Description = "Returns a list of associated courses with the class"
    )]
    [ProducesResponseType(typeof(ClassCoursesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClassCoursesById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClassCoursesQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}/students")]
    [SwaggerOperation(
        Summary = "Get all enrolled Students in a Class",
        Description = "Returns a class detail with list of enrolled students in the class"
    )]
    [ProducesResponseType(typeof(ClassStudentsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClassStudentsById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClassStudentsQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost("{id:guid}/students")]
    [SwaggerOperation(Summary = "Enroll a Student in a Class")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> EnrollStudentInClass(
        Guid id,
        EnrollStudentInClassRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new EnrollStudentInClassCommand(id, Guid.Parse(request.StudentId), userContext.StaffId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }

    [HttpGet("{id:guid}", Name = nameof(GetClassById))]
    [SwaggerOperation(
        Summary = "Get a Class by ID",
        Description = "Returns a Class with the specified ID"
    )]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClassById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClassByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Class",
        Description = "Returns the newly created Class"
    )]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateClass(CreateClassRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateClassCommand(
            request.Title,
            request.CourseIds,
            request.Description,
            userContext.StaffId
        ).Sanitize();

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute(nameof(GetClassById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update a Class by ID",
        Description = "Returns the updated Class"
    )]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateClassById(
        Guid id,
        UpdateClassRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateClassCommand(id, request.Title, request.Description).Sanitize();

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a Class by ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClassById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteClassCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
