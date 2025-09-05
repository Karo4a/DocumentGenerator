using DocumentGenerator.Web.Models.DocumentProduct;
using DocumentGenerator.Web.Models.Party;

namespace DocumentGenerator.Web.Models.Document
{
    /// <summary>
    /// Модель стороны акта
    /// </summary>
    public class DocumentApiModel
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
        public PartyApiModel Seller { get; set; } = null!;

        /// <summary>
        /// Объект передачи данных покупателя
        /// </summary>
        public PartyApiModel Buyer { get; set; } = null!;

        /// <summary>
        /// Список товаров для документа <see cref="DocumentProductApiModel"/>
        /// </summary>
        public ICollection<DocumentProductApiModel> Products { get; set; } = [];
    }
}