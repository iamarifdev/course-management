namespace CourseManagement.Application.Students.GetStudentClassEnrollment;

public sealed record ClassResponse(Guid Id, string Title);
public sealed record StudentResponse(Guid Id, string FirstName, string LastName);
public sealed record StaffResponse(Guid Id, string? FirstName, string? LastName);

public sealed record StudentClassEnrollmentResponse(
    Guid Id,
    ClassResponse Class,
    StudentResponse Student,
    StaffResponse EnrolledBy,
    DateTime EnrolledAt
);
