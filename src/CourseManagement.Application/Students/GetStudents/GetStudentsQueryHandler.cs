using CourseManagement.Application.Base;
using CourseManagement.Application.Base.Extensions;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Application.Students.GetStudents;

internal sealed class GetStudentsQueryHandler(IStudentRepository repository)
    : IQueryHandler<GetStudentsQuery, PaginatedResult<StudentResponse>>
{
    public async Task<Result<PaginatedResult<StudentResponse>>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = repository.GetQueryable();
        List<string> sortFields = ["firstname", "lastname", "email"];

        if (!string.IsNullOrWhiteSpace(request.FilterText))
        {
            query = query.Where(x =>
                x.FirstName.Contains(request.FilterText) ||
                x.LastName.Contains(request.FilterText) ||
                ((string)x.User.Email).Contains(request.FilterText)
            );
        }

        var sortBy = request.SortBy?.ToLowerCase();
        query = !string.IsNullOrWhiteSpace(sortBy) &&
                sortFields.Any(prop => prop.Equals(sortBy, StringComparison.OrdinalIgnoreCase))
            ? sortBy switch
            {
                "firstname" => query.SortBy(x => x.FirstName, request.SortOrder),
                "lastname" => query.SortBy(x => x.LastName, request.SortOrder),
                "email" => query.SortBy(x => x.User.Email, request.SortOrder),
                _ => query
            }
            : query.SortBy(x => x.CreatedAt, request.SortOrder);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.GetSkipItems())
            .Take(request.GetItemsCount())
            .Select(x => new StudentResponse(
                x.Id,
                x.UserId,
                x.User.Email.Value,
                x.FirstName,
                x.LastName,
                x.StaffId,
                x.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(items.ToPaginatedResult(totalCount, request));
    }
}
