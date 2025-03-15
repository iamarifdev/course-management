using CourseManagement.Application.Base;

namespace CourseManagement.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginResponse>;
