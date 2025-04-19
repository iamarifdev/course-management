using CourseManagement.Domain.Staffs;

namespace CourseManagement.Infrastructure.Database.Repositories;

internal sealed class StaffRepository(ApplicationDbContext dbContext) : Repository<Staff>(dbContext), IStaffRepository
{
}
