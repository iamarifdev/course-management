namespace CourseManagement.Domain.CourseClasses;

public interface ICourseClassRepository
{
    IQueryable<CourseClass> GetQueryable();
    void Add(CourseClass courseClass);
}
