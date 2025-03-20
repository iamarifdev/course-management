using CourseManagement.Domain.Base;
using CourseManagement.Domain.Staffs;
using CourseManagement.Domain.Students;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(x => x.Email)
            .HasMaxLength(250)
            .HasConversion(email => email.Value, value => new Email(value))
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(100)
            .HasConversion(password => password.Value, value => new Password(value))
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion(role => role.ToString(), value => Enum.Parse<Role>(value))
            .IsRequired();
        
        builder.HasOne(u => u.Staff)
            .WithOne(s => s.User)
            .HasForeignKey<Staff>(s => s.UserId);

        builder.HasOne(u => u.Student)
            .WithOne(s => s.User)
            .HasForeignKey<Student>(s => s.UserId);
        
        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasIsDeletedFilter();
    }
}
