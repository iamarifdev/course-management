namespace CourseManagement.Application.Students.GetStudentClassEnrollment;

public sealed record ClassInfoResponse(Guid Id, string Title);
public sealed record StudentInfoResponse(Guid Id, string FirstName, string LastName);
public sealed record StaffInfoResponse(Guid Id, string? FirstName, string? LastName);

public sealed record StudentClassEnrollmentResponse(
    Guid Id,
    ClassInfoResponse ClassInfo,
    StudentInfoResponse Student,
    StaffInfoResponse EnrolledBy,
    DateTime EnrolledAt
);
