using DocumentGenerator.Web.Models.DocumentProduct;

namespace DocumentGenerator.Web.Models.Document
{
    /// <summary>
    /// Модель редактирования документа
    /// </summary>
    public class DocumentRequestApiModel(
)
    {
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
        /// Идентификатор продавца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid BuyerId { get; set; }

        /// <summary>
        /// Список товаров для документа <see cref="DocumentProductRequestApiModel"/>
        /// </summary>
        ICollection<DocumentProductRequestApiModel> Products { get; set; } = null!;
    }
}
