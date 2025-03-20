using CourseManagement.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class CourseConfiguration : BaseEntityTypeConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.StaffId)
            .IsRequired();

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.StaffId);
        
        builder.HasMany(x => x.CourseClasses)
            .WithOne(x => x.Course)
            .HasForeignKey(x => x.CourseId);

        builder.HasIndex(x => x.Title)
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
