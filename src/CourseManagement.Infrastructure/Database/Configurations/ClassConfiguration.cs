using CourseManagement.Domain.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class ClassConfiguration : BaseEntityTypeConfiguration<Class>
{
    public override void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("classes");

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.StaffId)
            .IsRequired();

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.Classes)
            .HasForeignKey(x => x.StaffId);

        builder.HasMany(x => x.CourseClasses)
            .WithOne(x => x.Class)
            .HasForeignKey(x => x.ClassId);

        builder.HasMany(x => x.EnrolledStudentClasses)
            .WithOne(x => x.Class)
            .HasForeignKey(x => x.ClassId);

        builder.HasIndex(x => x.Title)
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
