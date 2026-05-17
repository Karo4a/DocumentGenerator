using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations
{
    /// <summary>
    /// Описывает конфигурацию <see cref="User"/>
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Конфигурация стороны акта
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Salt)
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired()
                .HasConversion<string>();

            builder.HasIndex(
                    x => new { x.Email, x.Password },
                    $"IX_{nameof(User)}_{nameof(User.Email)}_{nameof(User.Password)}")
                .IsUnique()
                .HasFilter($"\"{nameof(User.DeletedAt)}\" IS NULL");
        }
    }
}
