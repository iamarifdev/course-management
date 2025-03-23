using CourseManagement.Domain.StudentCourses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class StudentCourseConfiguration : BaseEntityTypeConfiguration<StudentCourse>
{
    public override void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.ToTable("student_courses");

        builder.Property(x => x.StudentId)
            .IsRequired();

        builder.Property(x => x.CourseId)
            .IsRequired();
        
        builder.Property(x => x.StaffId)
            .IsRequired();
        
        builder.HasOne(x => x.Course)
            .WithMany(x => x.EnrolledStudentCourses)
            .HasForeignKey(x => x.CourseId);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.EnrolledCourses)
            .HasForeignKey(x => x.StudentId);

        builder.HasOne(x => x.EnrolledBy)
            .WithMany(x => x.EnrolledStudentCourses)
            .HasForeignKey(x => x.StaffId);
        
        builder.HasIndex(x => new { x.StudentId, x.CourseId })
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
