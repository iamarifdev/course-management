using CourseManagement.Application.Base;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Infrastructure.Database;

public static class Seeder
{
    private const string StaffPassword = "Staff12345";
    private const string StaffEmail = "staff@university.com";

    public static void Seed(this ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        // Seed default users
        if (context.Users.Any())
        {
            return;
        }

        var hashedPassword = passwordHasher.Hash(StaffPassword).Value;

        var user = User.Create(
            new Email(StaffEmail),
            Role.Staff,
            new Password(hashedPassword)
        );

        context.Users.Add(user);
        context.SaveChanges();
        
        // Seed default courses
        if (context.Courses.Any())
        {
            return;
        }

        List<Course> courses = [
            Course.Create("Programming", user.Id, "Programming Course"),
            Course.Create("Business", user.Id, "Business Course"),
            Course.Create("Marketing", user.Id, "Marketing Course"),
        ];
        context.Courses.AddRange(courses);
        context.SaveChanges();
    }
}
