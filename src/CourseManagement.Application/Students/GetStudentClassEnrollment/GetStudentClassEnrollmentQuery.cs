using CourseManagement.Application.Base;

namespace CourseManagement.Application.Students.GetStudentClassEnrollment;

public sealed record GetStudentClassEnrollmentQuery(Guid StudentId, Guid ClassId)
    : IQuery<StudentClassEnrollmentResponse>;
