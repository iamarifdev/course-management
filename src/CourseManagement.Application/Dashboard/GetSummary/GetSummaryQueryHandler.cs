using CourseManagement.Application.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;

namespace CourseManagement.Application.Dashboard.GetSummary;

internal sealed class GetSummaryQueryHandler(
    IStudentRepository studentRepository,
    ICourseRepository courseRepository,
    IClassRepository classRepository,
    IStaffRepository staffRepository
) : IQueryHandler<GetSummaryQuery, DashboardSummaryResponse>
{
    public async Task<Result<DashboardSummaryResponse>> Handle(GetSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var studentsCount = await studentRepository.CountAsync(cancellationToken);
        var coursesCount = await courseRepository.CountAsync(cancellationToken);
        var classesCount = await classRepository.CountAsync(cancellationToken);
        var staffsCount = await staffRepository.CountAsync(cancellationToken);

        var summary = new DashboardSummaryResponse(
            studentsCount,
            coursesCount,
            classesCount,
            staffsCount
        );

        return Result.Success(summary);
    }
}
