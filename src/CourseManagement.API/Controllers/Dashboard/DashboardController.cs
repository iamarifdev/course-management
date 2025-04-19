using CourseManagement.Application.Base;
using CourseManagement.Application.Dashboard;
using CourseManagement.Application.Dashboard.GetSummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseManagement.API.Controllers.Dashboard;

[Authorize(Roles = Roles.Staff)]
[ApiController]
[Route("[controller]")]
public class DashboardController(ISender sender) : ControllerBase
{
    [HttpGet("summary")]
    [SwaggerOperation(
        Summary = "Get dashboard summary",
        Description = "Returns a summary of the dashboard containing the number " +
                      "of students, courses, classes, and staff members."
    )]
    [ProducesResponseType(typeof(PaginatedResult<DashboardSummaryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSummary(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetSummaryQuery(), cancellationToken);
        return Ok(result.Value);
    }
}
