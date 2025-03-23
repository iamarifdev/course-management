using CourseManagement.Domain.StudentCourseClasses;
using CourseManagement.Domain.StudentCourses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class StudentCourseClassConfiguration : BaseEntityTypeConfiguration<StudentCourseClass>
{
    public override void Configure(EntityTypeBuilder<StudentCourseClass> builder)
    {
        builder.ToTable("student_course_classes");

        builder.Property(x => x.StudentId)
            .IsRequired();

        builder.Property(x => x.CourseId)
            .IsRequired();

        builder.Property(x => x.ClassId)
            .IsRequired();
        
        builder.Property(x => x.StaffId)
            .IsRequired();
        
        builder.HasOne(x => x.Course)
            .WithMany(x => x.EnrolledStudentClasses)
            .HasForeignKey(x => x.ClassId);
        
        builder.HasOne(x => x.Class)
            .WithMany(x => x.EnrolledStudentClasses)
            .HasForeignKey(x => x.ClassId);

        builder.HasOne(x => x.Student)
            .WithMany(w => w.EnrolledClasses)
            .HasForeignKey(x => x.StudentId);

        builder.HasOne(x => x.EnrolledBy)
            .WithMany(x => x.EnrolledStudentClasses)
            .HasForeignKey(x => x.StaffId);
        
        builder.HasIndex(x => new { x.StudentId, x.CourseId, x.ClassId })
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
