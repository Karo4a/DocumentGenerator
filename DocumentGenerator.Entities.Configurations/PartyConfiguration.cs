using DocumentGenerator.Entities.ValidationConstants;
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
                .HasMaxLength(PartyValidationConstants.NameMaxLength);

            builder.Property(x => x.Job)
                .IsRequired()
                .HasMaxLength(PartyValidationConstants.JobMaxLength);

            builder.Property(x => x.TaxId)
                .IsRequired()
                .HasMaxLength(PartyValidationConstants.TaxIdMaxLength);

            builder.HasIndex(x => x.TaxId, $"IX_{nameof(Party)}_{nameof(Party.TaxId)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Party.DeletedAt)}\" IS NULL");
        }
    }
}
