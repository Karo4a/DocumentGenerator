using DocumentGenerator.Services.Contracts.Models.Product;

namespace DocumentGenerator.Services.Contracts.Models.DocumentProduct
{
    /// <summary>
    /// Модель документа
    /// </summary>
    public class DocumentProductModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Объект передачи данных <see cref="ProductModel"/>
        /// </summary>
        public ProductModel Product { get; set; } = null!;

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена товара на момент создания документа
        /// </summary>
        public decimal Cost { get; set; }
    }
}
