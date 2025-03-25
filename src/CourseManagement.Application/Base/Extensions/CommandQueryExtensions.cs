using System.Reflection;

namespace CourseManagement.Application.Base.Extensions;

public static class CommandQueryExtensions
{
    public static T Sanitize<T>(this T command)
        where T : class
    {
        var type = command.GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType == typeof(string) && p.CanRead)
            .ToArray();

        if (!properties.Any())
        {
            return command;
        }

        var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .OrderByDescending(c => c.GetParameters().Length)
            .FirstOrDefault();

        if (constructor == null)
        {
            return command;
        }

        var parameters = constructor.GetParameters();
        var parameterValues = new object?[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];
            var property = properties.FirstOrDefault(p => p.Name.Equals(param.Name, StringComparison.OrdinalIgnoreCase));
            parameterValues[i] = property != null
                ? ((string?)property.GetValue(command))?.Trim()
                : type.GetProperty(param.Name!, BindingFlags.Public | BindingFlags.Instance)?.GetValue(command);
        }

        return (T)constructor.Invoke(parameterValues);
    }
}
