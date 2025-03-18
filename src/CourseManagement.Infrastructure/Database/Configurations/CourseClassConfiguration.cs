using CourseManagement.Domain.CourseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

public class CourseClassConfiguration : BaseEntityTypeConfiguration<CourseClass>
{
    public override void Configure(EntityTypeBuilder<CourseClass> builder)
    {
        builder.ToTable("course_classes");

        builder.Property(x => x.CourseId)
            .IsRequired();
        
        builder.Property(x => x.ClassId)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();
        
        builder.HasOne(cc => cc.Course)
            .WithMany(c => c.CourseClasses)
            .HasForeignKey(cc => cc.CourseId);

        builder.HasOne(cc => cc.Class)
            .WithMany(c => c.CourseClasses)
            .HasForeignKey(cc => cc.ClassId);
        
        builder.HasIndex(x => new { x.CourseId, x.ClassId })
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
