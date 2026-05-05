using DocumentGenerator.Api.Models.DocumentProduct;

namespace DocumentGenerator.Api.Models.Document
{
    /// <summary>
    /// Модель редактирования документа
    /// </summary>
    public class DocumentRequestApiModel : DocumentBaseApiModel
    {
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
        public ICollection<DocumentProductRequestApiModel> Products { get; set; } = null!;
    }
}
