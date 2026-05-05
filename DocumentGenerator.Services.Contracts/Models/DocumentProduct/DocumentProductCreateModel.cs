namespace DocumentGenerator.Services.Contracts.Models.DocumentProduct
{
    /// <summary>
    /// Модель создания документа
    /// </summary>
    public class DocumentProductCreateModel
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
