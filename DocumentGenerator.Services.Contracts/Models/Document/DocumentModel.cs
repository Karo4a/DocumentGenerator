using DocumentGenerator.Services.Contracts.Models.DocumentProduct;
using DocumentGenerator.Services.Contracts.Models.Party;

namespace DocumentGenerator.Services.Contracts.Models.Document
{
    /// <summary>
    /// Модель документа
    /// </summary>
    public class DocumentModel
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
        /// Объект передачи данных продавца
        /// </summary>
        public PartyModel Seller { get; set; } = null!;

        /// <summary>
        /// Объект передачи данных покупателя
        /// </summary>
        public PartyModel Buyer { get; set; } = null!;

        /// <summary>
        /// Список товаров для документа <see cref="DocumentProductModel"/>
        /// </summary>
        public ICollection<DocumentProductModel> Products { get; set; } = [];
    }
}
