using CourseManagement.Application.Students;

namespace CourseManagement.Application.Courses.GetCourseStudents;

public record CourseStudentsResponse(
    Guid Id,
    string Title,
    string? Description,
    List<StudentResponse> Students,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
