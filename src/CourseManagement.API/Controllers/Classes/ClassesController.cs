using CourseManagement.Application.Base;
using CourseManagement.Application.Classes.CreateClass;
using CourseManagement.Application.Classes.DeleteClass;
using CourseManagement.Application.Classes.GetClassById;
using CourseManagement.Application.Classes.GetClassCourses;
using CourseManagement.Application.Classes.GetClasses;
using CourseManagement.Application.Classes.UpdateClass;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Controllers.Classes;

[ApiController]
[Route("api/classes")]
public class ClassesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetAllClasses(
        [FromQuery] GetAllClassesRequest request,
        CancellationToken cancellationToken)
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
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetClassCoursesById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetClassCoursesQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpGet("{id:guid}", Name = "GetClassById")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> GetClassById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClassByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> CreateClass(
        CreateClassRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateClassCommand(request.Title, request.CourseIds, request.Description);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure
            ? result.ToErrorResult()
            : CreatedAtRoute("GetClassById", new { id = result.Value }, null);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> UpdateClassById(
        Guid id,
        UpdateClassRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClassCommand(id, request.Title, request.Description);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.Staff)]
    public async Task<IActionResult> DeleteClassById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClassCommand(id);

        var result = await sender.Send(command, cancellationToken);
        return result.IsFailure ? result.ToErrorResult() : Ok();
    }
}
