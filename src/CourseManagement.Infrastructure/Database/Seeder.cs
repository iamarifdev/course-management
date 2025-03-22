using Bogus;
using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Domain.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Infrastructure.Database;

public static class Seeder
{
    private const string StaffPassword = "Staff12345";
    private const string StudentPassword = "Student12345";
    private const string StaffEmail = "staff@university.com";

    private static IPasswordHasher PasswordHasher { get; set; }

    private static User DefaultUser { get; set; }

    private static Staff DefaultStaff { get; set; }

    public static void Seed(this ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        PasswordHasher = passwordHasher;

        DefaultUser = User.Create(
            new Email(StaffEmail),
            Role.Staff,
            new Password(passwordHasher.Hash(StaffPassword).Value)
        );
        DefaultStaff = Staff.Create(
            DefaultUser.Id,
            "Admin",
            null,
            null,
            "Computer Science and Engineering"
        );

        SeedStaff(context);
        SeedCoursesAndClasses(context);
        SeedStudents(context);
    }

    private static void SeedStaff(ApplicationDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        context.Users.Add(DefaultUser);
        context.Staffs.Add(DefaultStaff);
        context.SaveChanges();
    }

    private static void SeedCoursesAndClasses(ApplicationDbContext context)
    {
        if (context.Courses.Any())
        {
            return;
        }

        // Seed Courses
        var courses = GetCourseTitles().Select(title => Course.Create(
            title: title,
            staffId: DefaultStaff.Id,
            description: title
        )).ToList();
        context.Courses.AddRange(courses);
        context.SaveChanges();

        // Seed Classes and CourseClasses
        var classFaker = new Faker<Class>()
            .CustomInstantiator(f => Class.Create(
                title: $"CS{f.Random.Number(100, 999)}",
                staffId: DefaultStaff.Id,
                courseIds: [.. f.PickRandom(courses, f.Random.Number(1, 3)).Select(c => c.Id)],
                description: f.Lorem.Sentence(8)
            ));

        var classes = classFaker.Generate(20);
        context.Classes.AddRange(classes);
        context.SaveChanges();
    }

    private static void SeedStudents(ApplicationDbContext context)
    {
        if (context.Students.Any())
        {
            return;
        }

        var studentFaker = new Faker<Student>()
            .CustomInstantiator(f =>
            {
                var user = User.Create(
                    new Email(f.Internet.Email(f.Name.FirstName(), f.Name.LastName())
                        .ToLowerCase()),
                    Role.Student,
                    new Password(PasswordHasher.Hash(StudentPassword).Value)
                );
                context.Users.Add(user);

                return Student.Create(
                    userId: user.Id,
                    firstName: f.Name.FirstName(),
                    lastName: f.Name.LastName(),
                    staffId: DefaultStaff.Id
                );
            });

        var students = studentFaker.Generate(100);
        context.Students.AddRange(students);
        context.SaveChanges();
    }

    private static List<string> GetCourseTitles()
    {
        return
        [
            "Introduction to Programming",
            "Data Structures",
            "Algorithms",
            "Operating Systems",
            "Database Systems",
            "Computer Networks",
            "Software Engineering",
            "Artificial Intelligence",
            "Machine Learning",
            "Web Development",
            "Computer Graphics",
            "Cybersecurity",
            "Cloud Computing",
            "Mobile Application Development",
            "Parallel Computing",
            "Computer Architecture",
            "Theory of Computation",
            "Distributed Systems",
            "Computer Vision",
            "Natural Language Processing"
        ];
    }
}
