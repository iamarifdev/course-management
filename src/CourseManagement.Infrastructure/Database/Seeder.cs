using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Infrastructure.Database;

public static class Seeder
{
    private const string StaffPassword = "Staff12345";
    private const string StaffEmail = "staff@university.com";

    public static void Seed(this ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        if (context.Users.Any()) return;

        var hashedPassword = passwordHasher.Hash(StaffPassword).Value;

        var user = User.Create(
            new Name("Test", "Staff"),
            new Email(StaffEmail),
            Role.Staff,
            new Password(hashedPassword)
        );

        context.Users.Add(user);
        context.SaveChanges();
    }
}