using DocumentGenerator.Web.Models.Product;

namespace DocumentGenerator.Web.Models.DocumentProduct
{
    /// <summary>
    /// Модель документа
    /// </summary>
    public class DocumentProductApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Объект передачи данных <see cref="ProductApiModel"/>
        /// </summary>
        public ProductApiModel Product { get; set; } = null!;

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