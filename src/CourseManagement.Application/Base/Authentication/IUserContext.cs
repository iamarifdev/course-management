namespace CourseManagement.Application.Base.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
    string Email { get; }
    string Role { get; }
}
