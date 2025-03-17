using CourseManagement.Application.Courses.CreateCourse;
using CourseManagement.Domain.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class CoursesController(ISender sender) : ControllerBase
{
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

        return Ok(result.Value.Id);
    }
}
