using CourseManagement.Application.Courses;

namespace CourseManagement.Application.Classes.GetClassCourses;

public sealed record ClassCoursesResponse(
    Guid Id,
    string Name,
    string? Description,
    List<CourseResponse> Courses,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
