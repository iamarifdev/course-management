using CourseManagement.Domain.Base;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Users;

namespace CourseManagement.Domain.Students;

public sealed class Student : Entity
{
    private Student(Guid id, Guid userId, string firstName, string lastName, Guid staffId) : base(id)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        StaffId = staffId;
    }

    private Student() { }

    public Guid UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid StaffId { get; private set; }

    public User User { get; set; }
    public Staff AddedBy { get; set; }

    public static Student Create(Guid userId, string firstName, string lastName, Guid createdBy)
    {
        return new Student(Guid.NewGuid(), userId, firstName, lastName, createdBy);
    }
}
