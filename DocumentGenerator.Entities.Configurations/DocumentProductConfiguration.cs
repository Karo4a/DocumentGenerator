using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations
{
    /// <summary>
    /// Описывает конфигурацию <see cref="DocumentProduct"/>
    /// </summary>
    public class DocumentProductConfiguration : IEntityTypeConfiguration<DocumentProduct>
    {
        /// <summary>
        /// Конфигурация товара для строчки в документе
        /// </summary>
        public void Configure(EntityTypeBuilder<DocumentProduct> builder)
        {
            builder.ToTable("DocumentProducts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.DocumentId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Cost)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Document)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
