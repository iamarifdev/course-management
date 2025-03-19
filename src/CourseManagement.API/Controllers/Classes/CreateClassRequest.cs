namespace CourseManagement.API.Controllers.Classes;

public record CreateClassRequest(string Name, List<Guid> CourseIds, string? Description);
