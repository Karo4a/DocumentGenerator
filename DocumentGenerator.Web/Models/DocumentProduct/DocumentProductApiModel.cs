using DocumentGenerator.Web.Models.Product;

namespace DocumentGenerator.Web.Models.DocumentProduct
{
    /// <summary>
    /// Модель документа
    /// </summary>
    public class DocumentProductApiModel : DocumentProductBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Объект передачи данных <see cref="ProductApiModel"/>
        /// </summary>
        public ProductApiModel Product { get; set; } = null!;
    }
}