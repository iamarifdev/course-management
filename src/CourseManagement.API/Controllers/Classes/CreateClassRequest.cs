namespace CourseManagement.API.Controllers.Classes;

public record CreateClassRequest(string Title, List<Guid> CourseIds, string? Description);
