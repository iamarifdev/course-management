using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name, Email email, Role role, Password password) : base(id)
    {
        Name = name;
        Email = email;
        Role = role;
        Password = password;
    }

#pragma warning disable CS8618, CS9264
    // Require by EF Core to run the app without any error
    private User()
    {
    }
#pragma warning restore CS8618, CS9264

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Role Role { get; private set; }
    public Password Password { get; private set; }

    public static User Create(Name name, Email email, Role role, Password password)
    {
        var user = new User(Guid.NewGuid(), name, email, role, password);
        return user;
    }
}