using CourseManagement.Application.Base;
using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;

namespace CourseManagement.Application.Dashboard.GetSummary;

public sealed record GetSummaryQuery : IQuery<DashboardSummaryResponse>;

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
        var result = await Task.WhenAll(
            studentRepository.CountAsync(cancellationToken),
            courseRepository.CountAsync(cancellationToken),
            classRepository.CountAsync(cancellationToken),
            staffRepository.CountAsync(cancellationToken)
        );

        var summary = new DashboardSummaryResponse(
            result[0],
            result[1],
            result[2],
            result[3]
        );
        return Result.Success(summary);
    }
}
