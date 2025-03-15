using CourseManagement.Domain.Base;
using CourseManagement.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseManagement.Domain.Users.ValueObjects;

namespace CourseManagement.Infrastructure.Database.Configurations;

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.OwnsOne(x => x.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();
            nameBuilder.Property(n => n.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Property(x => x.Email)
            .HasMaxLength(250)
            .HasConversion(email => email.Value, value => new Email(value))
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(60)
            .HasConversion(password => password.Value, value => new Password(value))
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion(role => role.ToString(), value => Enum.Parse<Role>(value))
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasFilter("\"is_deleted\" = false");
    }
}