using CourseManagement.API.Extensions;
using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Base.Extensions;
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

namespace CourseManagement.API.Controllers.Students;

[Authorize(Roles = Roles.Staff)]
[ApiController]
[Route("[controller]")]
public class StudentsController(ISender sender, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllStudents(
        [FromQuery] GetAllStudentsRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetStudentsQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            FilterText = request.FilterText,
            SortBy = request.SortBy,
            SortOrder = request.SortOrder
        }.Sanitize();

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}", Name = nameof(GetStudentById))]
    public async Task<IActionResult> GetStudentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetStudentByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}/classes/{classId:guid}/enrollment")]
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
    public async Task<IActionResult> GetClassmates(CancellationToken cancellationToken)
    {
        var query = new GetClassmatesQuery(userContext.StudentId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [Authorize(Roles = Roles.Student)]
    [HttpGet("me/course-classes")]
    public async Task<IActionResult> GetMyCourseClasses(CancellationToken cancellationToken)
    {
        var query = new GetStudentCourseClassesQuery(userContext.StudentId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
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

    [HttpPatch("{id:guid}")]
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStudentById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteStudentCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
