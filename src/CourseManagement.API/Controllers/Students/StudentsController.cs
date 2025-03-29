using CourseManagement.API.Extensions;
using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Application.Students;
using CourseManagement.Application.Students.AddStudent;
using CourseManagement.Application.Students.DeleteStudent;
using CourseManagement.Application.Students.GetClassmates;
using CourseManagement.Application.Students.GetStudentById;
using CourseManagement.Application.Students.GetStudentClassEnrollment;
using CourseManagement.Application.Students.GetStudentCourseClasses;
using CourseManagement.Application.Students.GetStudents;
using CourseManagement.Application.Students.UpdateStudent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseManagement.API.Controllers.Students;

[ApiController]
[Route("[controller]")]
public class StudentsController(ISender sender, IUserContext userContext) : ControllerBase
{
    [Authorize(Roles = Roles.Staff)]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Students by pagination, filtering and sorting",
        Description = "Returns a list of paginated Students"
    )]
    [ProducesResponseType(typeof(PaginatedResult<StudentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStudents(
        [FromQuery] GetAllStudentsRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetStudentsQuery(
            request.FilterText,
            request.SortBy,
            request.SortOrder,
            request.PageNumber,
            request.PageSize
        ).Sanitize();

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Staff)]
    [HttpGet("{id:guid}", Name = nameof(GetStudentById))]
    [SwaggerOperation(
        Summary = "Get a Student by ID",
        Description = "Returns a Student with the specified ID"
    )]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetStudentByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Staff)]
    [HttpGet("{id:guid}/classes/{classId:guid}/enrollment")]
    [SwaggerOperation(
        Summary = "Get Student-Class enrollment details",
        Description = "Returns a Class enrollment details including when and who enrolled the student"
    )]
    [ProducesResponseType(typeof(StudentClassEnrollmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentClassEnrollment(
        Guid id,
        Guid classId,
        CancellationToken cancellationToken
    )
    {
        var query = new GetStudentClassEnrollmentQuery(id, classId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Student)]
    [HttpGet("me/classmates")]
    [SwaggerOperation(
        Summary = "Get Classmates of a Student",
        Description = "Returns a list of Students who are in the same class as the authenticated Student"
    )]
    [ProducesResponseType(typeof(List<ClassmatesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClassmates(CancellationToken cancellationToken)
    {
        var query = new GetClassmatesQuery(userContext.StudentId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Student)]
    [HttpGet("me/course-classes")]
    [SwaggerOperation(
        Summary = "Get enrolled Courses and Classes of a Student",
        Description = "Returns a list of Courses and Classes that the authenticated Student is enrolled in"
    )]
    [ProducesResponseType(typeof(List<StudentCourseClassResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyCourseClasses(CancellationToken cancellationToken)
    {
        var query = new GetStudentCourseClassesQuery(userContext.StudentId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Staff)]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Add a new Student",
        Description = "Returns the newly added Student"
    )]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddStudent(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var command = new AddStudentCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            userContext.StaffId
        ).Sanitize();

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute(nameof(GetStudentById), new { id = result.Value.Id }, result.Value);
    }

    [Authorize(Roles = Roles.Staff)]
    [HttpPatch("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update a Student by ID",
        Description = "Returns the updated Student"
    )]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateStudentById(
        Guid id,
        UpdateStudentRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateStudentCommand(
            id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        ).Sanitize();

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Staff)]
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a Student by ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStudentById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteStudentCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
