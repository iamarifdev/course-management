using System.Globalization;

namespace CourseManagement.Application.Base.Extensions;

public static class StringExtensions
{
    public static string ToLowerCase(this string input)
    {
        return input.ToLower(CultureInfo.GetCultureInfo("en-US"));
    }
}
