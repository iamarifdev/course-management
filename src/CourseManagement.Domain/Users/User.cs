using CourseManagement.Domain.Base;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Email email, Role role, Password password) : base(id)
    {
        Email = email;
        Role = role;
        Password = password;
    }

    private User() { }

    public Email Email { get; private set; }
    public Role Role { get; private set; }
    public Password Password { get; private set; }

    public Staff? Staff { get; set; }
    public Student? Student { get; set; }

    public static User Create(Email email, Role role, Password password)
    {
        var user = new User(Guid.NewGuid(), email, role, password);
        return user;
    }
}
