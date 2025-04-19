namespace CourseManagement.Application.Dashboard;

public sealed record DashboardSummaryResponse(
    int StudentCount,
    int CourseCount,
    int ClassCount,
    int StaffCount
);
