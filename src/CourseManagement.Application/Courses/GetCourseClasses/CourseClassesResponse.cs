using CourseManagement.Application.Classes;

namespace CourseManagement.Application.Courses.GetCourseClasses;

public record CourseClassesResponse(
    Guid Id,
    string Name,
    string? Description,
    List<ClassResponse> Classes,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
