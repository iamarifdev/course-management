using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Authentication;
using CourseManagement.Application.Students.AddStudent;
using CourseManagement.Application.Students.DeleteStudent;
using CourseManagement.Application.Students.GetClassmates;
using CourseManagement.Application.Students.GetStudentById;
using CourseManagement.Application.Students.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Students;

[ApiController]
[Route("api/students")]
public class StudentsController(ISender sender, IUserContext userContext) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetAllStudents(
        [FromQuery] GetAllStudentsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetStudentsQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            FilterText = request.FilterText,
            SortBy = request.SortBy,
            SortOrder = request.SortOrder
        };

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }
    
    [HttpGet("{id:guid}", Name = nameof(GetStudentById))]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetStudentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetStudentByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }
    
    [HttpGet("me/classmates")]
    [Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> GetClassmates(CancellationToken cancellationToken)
    {
        var query = new GetClassmatesQuery(userContext.StudentId);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> AddStudent(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var command = new AddStudentCommand(request.FirstName, request.LastName, request.Email, request.Password);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute(nameof(GetStudentById), new { id = result.Value }, null);
    }
    

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> DeleteStudentById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteStudentCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
