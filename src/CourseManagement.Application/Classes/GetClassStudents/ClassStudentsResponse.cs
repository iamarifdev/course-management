using CourseManagement.Application.Students;

namespace CourseManagement.Application.Classes.GetClassStudents;

public record ClassStudentsResponse(
    Guid Id,
    string Title,
    string? Description,
    List<StudentResponse> Students,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
