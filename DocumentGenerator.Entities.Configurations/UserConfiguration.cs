using DocumentGenerator.Entities.Default;
using DocumentGenerator.Entities.ValidationConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию <see cref="User"/>
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Конфигурация пользователя
    /// </summary>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Login)
            .IsRequired()
            .HasMaxLength(UserValidationConstants.LoginMaxLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(UserValidationConstants.EmailMaxLength);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(UserValidationConstants.PasswordHashMaxLength);

        builder.Property(x => x.PasswordSalt)
            .IsRequired()
            .HasMaxLength(UserValidationConstants.PasswordSaltMaxLength);

        builder.Property(x => x.SecurityStamp)
            .IsRequired();

        builder.Property(x => x.UserRoleId)
            .IsRequired();

        builder.HasOne(x => x.UserRole)
            .WithMany()
            .HasForeignKey(x => x.UserRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(
                x => x.Login,
                $"IX_{nameof(User)}_{nameof(User.Login)}")
            .IsUnique()
            .HasFilter($"\"{nameof(User.DeletedAt)}\" IS NULL");

        builder.HasData(UsersDefault.Admin);
    }
}
