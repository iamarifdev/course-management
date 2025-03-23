using CourseManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class StudentConfiguration : BaseEntityTypeConfiguration<Student> 
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");
        
        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.StaffId)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithOne(x => x.Student)
            .HasForeignKey<Student>(x => x.UserId);

        builder.HasOne(x => x.AddedBy)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.EnrolledClasses)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.StudentId);

        builder.HasMany(x => x.EnrolledCourses)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.StudentId);
        
        builder.HasIndex(x => x.UserId)
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
