using DocumentGenerator.Entities.Enums;
using DocumentGenerator.Entities.ValidationConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations
{
    /// <summary>
    /// Описывает конфигурацию <see cref="UserRole"/>
    /// </summary>
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// Конфигурация роли пользователя
        /// </summary>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(UserRoleValidationConstants.UserRoleMaxLength);

            builder.HasData(Enum.GetValues<Role>().Select(
                x => new UserRole()
                {
                    Id = Guid.NewGuid(),
                    Role = x,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }));
        }
    }
}
