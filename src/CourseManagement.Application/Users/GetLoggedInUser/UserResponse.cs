namespace CourseManagement.Application.Users.GetLoggedInUser;

public sealed record UserResponse(Guid Id, string Email, string FirstName, string LastName);