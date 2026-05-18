using DocumentGenerator.Entities;
using DocumentGenerator.Entities.Contracts;

namespace DocumentGenerator.Repositories.Contracts.Models
{
    /// <summary>
    /// Модель документа для запроса из базы данных
    /// </summary>
    public class DocumentDbModel : IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Номер основного договора
        /// </summary>
        public string ContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// Дата подписания документа
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Навигационное свойство продавца
        /// </summary>
        public Party Seller { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство покупателя
        /// </summary>
        public Party Buyer { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство списка <see cref="DocumentProductDbModel"/>
        /// </summary>
        public ICollection<DocumentProductDbModel> Products { get; set; } = [];
    }
}
