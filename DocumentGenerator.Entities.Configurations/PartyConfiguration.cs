using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations
{
    /// <summary>
    /// Описывает конфигурацию <see cref="Party"/>
    /// </summary>
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        /// <summary>
        /// Конфигурация стороны акта
        /// </summary>
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.ToTable("Parties");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Job)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.TaxId)
                .IsRequired()
                .HasMaxLength(12);

            builder.HasIndex(x => x.Name, $"IX_{nameof(Party)}_{nameof(Party.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Party.DeletedAt)}\" IS NULL");
        }
    }
}
