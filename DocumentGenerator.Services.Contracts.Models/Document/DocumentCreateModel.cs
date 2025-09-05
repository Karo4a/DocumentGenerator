using DocumentGenerator.Services.Contracts.Models.DocumentProduct;

namespace DocumentGenerator.Services.Contracts.Models.Document
{
    /// <summary>
    /// Модель создания документа
    /// </summary>
    public class DocumentCreateModel
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
        /// Список товаров для документа <see cref="DocumentProductCreateModel"/>
        /// </summary>
        public ICollection<DocumentProductCreateModel> Products { get; set; } = [];
    }
}
