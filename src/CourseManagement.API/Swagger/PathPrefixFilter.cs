using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CourseManagement.API.Swagger;

internal sealed class PathPrefixFilter(string prefix) : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths.ToDictionary(
            entry => entry.Key,
            entry => entry.Value
        );

        swaggerDoc.Paths.Clear();

        foreach (var (key, value) in paths)
        {
            swaggerDoc.Paths.Add($"{prefix}{key}", value);
        }
    }
}
