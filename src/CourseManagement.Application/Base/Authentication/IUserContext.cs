namespace CourseManagement.Application.Base.Authentication;

public interface IUserContext
{
    Guid StaffId { get; }
    Guid StudentId { get; }
    Guid UserId { get; }
    string Email { get; }
    string Role { get; }
}
