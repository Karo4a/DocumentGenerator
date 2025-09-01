using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentGenerator.Entities.Configurations
{
    /// <summary>
    /// Описывает конфигурацию <see cref="Product"/>
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Конфигурация товара
        /// </summary>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(x => x.Name, $"IX_{nameof(Product)}_{nameof(Product.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Product.DeletedAt)}\" IS NULL");
        }
    }
}
