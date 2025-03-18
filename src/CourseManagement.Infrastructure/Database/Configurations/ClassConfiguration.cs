using CourseManagement.Domain.Classes;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

public class ClassConfiguration : BaseEntityTypeConfiguration<Class>
{
    public override void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("classes");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy);

        builder.HasMany(c => c.CourseClasses)
            .WithOne(cc => cc.Class)
            .HasForeignKey(cc => cc.ClassId);

        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
