using CourseManagement.Domain.Courses;
using CourseManagement.Domain.Courses.ValueObjects;
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
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .HasConversion(description => description!.Value, value => new Description(value))
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
