using DocumentGenerator.Entities.Contracts;

namespace DocumentGenerator.Entities
{
    /// <summary>
    /// Сущность документа
    /// </summary>
    public class Document : DbBaseEntity
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber {  get; set; } = string.Empty;

        /// <summary>
        /// Номер основного договора
        /// </summary>
        public string ContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// Дата подписания документа
        /// </summary>
        public DateOnly Date {  get; set; }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid BuyerId { get; set; }

        /// <summary>
        /// Навигационное свойство продавца
        /// </summary>
        public Party Seller { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство покупателя
        /// </summary>
        public Party Buyer { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство списка <see cref="DocumentProduct"/>
        /// </summary>
        public ICollection<DocumentProduct> Products { get; set; } = [];
    }
}
