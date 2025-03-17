using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Application.Courses.GetCourseById;
using CourseManagement.Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class CoursesController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}", Name = "GetCourseById")]
    [Authorize]
    public async Task<IActionResult> GetCourseById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(request.Name, request.Description);

        var result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtRoute("GetCourseById", new { id = result.Value }, null);
    }
}
