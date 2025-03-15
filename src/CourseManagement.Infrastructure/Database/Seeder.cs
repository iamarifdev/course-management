using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Infrastructure.Database;

public static class Seeder
{
    private const string StaffPassword = "$Staff12345$";
    private const string StaffEmail = "staff@university.com";
    
    public static void Seed(this ApplicationDbContext context)
    {
        if (context.Users.Any()) return;

        var user = User.Create(
            new Name("Staff", "At University"),
            new Email(StaffEmail),
            Role.Staff,
            Password.Hash(StaffPassword).Value
        );
        
        context.Users.Add(user);
        context.SaveChanges();
    }
}