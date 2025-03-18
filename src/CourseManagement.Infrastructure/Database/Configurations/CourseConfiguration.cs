using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagement.Infrastructure.Database.Configurations;

public class CourseConfiguration : BaseEntityTypeConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

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
        
        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasFilter("\"is_deleted\" = false");
    }
}
