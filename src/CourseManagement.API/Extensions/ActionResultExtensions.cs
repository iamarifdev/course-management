using CourseManagement.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.API.Extensions;

public static class ActionResultExtensions
{
    public static ObjectResult ToErrorResult(this Result result)
    {
        return result.Error.Type switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(result.Error),
            ErrorType.Conflict => new ConflictObjectResult(result.Error),
            ErrorType.Validation or ErrorType.Failure or ErrorType.Problem => new BadRequestObjectResult(result.Error),
            _ => new ObjectResult(result.Error) { StatusCode = 500 },
        };
    }
}
