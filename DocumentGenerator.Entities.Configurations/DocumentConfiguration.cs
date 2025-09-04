using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations
{
    /// <summary>
    /// Описывает конфигурацию <see cref="Document"/>
    /// </summary>
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        /// <summary>
        /// Конфигурация товара для строчки в документе
        /// </summary>
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DocumentNumber)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.ContractNumber)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Date)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.SellerId)
                .IsRequired();

            builder.Property(x => x.BuyerId)
                .IsRequired();

            builder.HasOne(x => x.Seller)
                .WithMany()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Buyer)
                .WithMany()
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
