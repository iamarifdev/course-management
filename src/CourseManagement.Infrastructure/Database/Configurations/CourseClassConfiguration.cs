using CourseManagement.Domain.CourseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class CourseClassConfiguration : BaseEntityTypeConfiguration<CourseClass>
{
    public override void Configure(EntityTypeBuilder<CourseClass> builder)
    {
        builder.ToTable("course_classes");

        builder.Property(x => x.CourseId)
            .IsRequired();
        
        builder.Property(x => x.ClassId)
            .IsRequired();

        builder.Property(x => x.StaffId)
            .IsRequired();
        
        builder.HasOne(x => x.Course)
            .WithMany(x => x.CourseClasses)
            .HasForeignKey(x => x.CourseId);

        builder.HasOne(x => x.Class)
            .WithMany(x => x.CourseClasses)
            .HasForeignKey(x => x.ClassId);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.CourseClasses)
            .HasForeignKey(x => x.StaffId);
        
        builder.HasIndex(x => new { x.CourseId, x.ClassId })
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
