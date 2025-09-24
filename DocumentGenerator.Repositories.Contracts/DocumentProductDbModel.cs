using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts
{
    /// <summary>
    /// Модель товара документа для запроса из базы данных
    /// </summary>
    public class DocumentProductDbModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Навигационное свойство <see cref="Entities.Product">
        /// </summary>
        public Product Product { get; set; } = null!;

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
