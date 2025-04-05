using System.Globalization;

namespace CourseManagement.Domain.Users.ValueObjects;

public record Email
{
    private static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("en-US");
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty or whitespace.", nameof(value));
        }

        Value = value.ToLower(Culture);
    }

    public static implicit operator Email(string value) => new(value);
    public static implicit operator string(Email email) => email.Value;

    public override string ToString() => Value;
}
