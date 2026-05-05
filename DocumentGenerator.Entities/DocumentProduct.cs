using DocumentGenerator.Entities.Contracts;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность товара для строчки в документе
    /// </summary>
    public class DocumentProduct : DbBaseEntity
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена товара на момент создания документа
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Навигационное свойство <see cref="Entities.Product"/>
        /// </summary>
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство <see cref="Entities.Document"/>
        /// </summary>
        public Document Document { get; set; } = null!;
    }
}
