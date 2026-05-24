using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations;

/// <summary>
/// Конфигурация EF Core для сущности <see cref="RefreshToken"/>
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>, IEntityConfiguration
{
    /// <summary>
    /// Конфигурация токена обновления
    /// </summary>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Expires)
            .IsRequired();

        builder.Property(x => x.SecurityStamp)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.DeletedAt);

        builder.HasIndex(x => x.UserId, $"IX_{nameof(RefreshToken)}_{nameof(RefreshToken.UserId)}")
            .IsUnique()
            .HasFilter($"\"{nameof(User.DeletedAt)}\" IS NULL");
    }
}
