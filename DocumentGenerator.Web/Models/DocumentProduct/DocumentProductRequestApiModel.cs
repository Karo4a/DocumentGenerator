namespace DocumentGenerator.Web.Models.DocumentProduct
{
    /// <summary>
    /// Модель редактирования документа
    /// </summary>
    public class DocumentProductRequestApiModel
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

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