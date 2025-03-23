using CourseManagement.Application.Base;

namespace CourseManagement.Application.Courses.EnrollStudentInCourse;

public record EnrollStudentInCourseCommand(Guid CourseId, Guid StudentId, Guid StaffId) : ICommand;
