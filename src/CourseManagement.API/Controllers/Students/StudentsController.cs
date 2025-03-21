using CourseManagement.Application.Base;
using CourseManagement.Application.Students.CreateStudent;
using CourseManagement.Application.Students.GetStudentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Students;

[ApiController]
[Route("api/students")]
public class StudentsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}", Name = nameof(GetStudentById))]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetStudentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetStudentByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> AddStudent(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateStudentCommand(request.FirstName, request.LastName, request.Email, request.Password);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute(nameof(GetStudentById), new { id = result.Value }, null);
    }
}
