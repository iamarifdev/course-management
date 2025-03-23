using CourseManagement.Domain.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class StaffConfiguration : BaseEntityTypeConfiguration<Staff> 
{
    public override void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("staffs");
        
        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(100);
        
        builder.Property(x => x.LastName)
            .HasMaxLength(100);
        
        builder.Property(x => x.Department)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.StaffId)
            .IsRequired(false);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Staff)
            .HasForeignKey<Staff>(x => x.UserId);

        // Self reference to track which staff added the staff
        builder.HasOne(x => x.AddedBy)
            .WithMany()
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.Classes)
            .WithOne(x => x.CreatedBy)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.Courses)
            .WithOne(x => x.CreatedBy)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.CourseClasses)
            .WithOne(x => x.CreatedBy)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.Students)
            .WithOne(x => x.AddedBy)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.EnrolledStudentCourses)
            .WithOne(x => x.EnrolledBy)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.EnrolledStudentClasses)
            .WithOne(x => x.EnrolledBy)
            .HasForeignKey(x => x.StaffId);
        
        builder.HasIndex(x => x.UserId)
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
