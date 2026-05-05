using DocumentGenerator.Web.Models.DocumentProduct;
using DocumentGenerator.Web.Models.Party;

namespace DocumentGenerator.Web.Models.Document
{
    /// <summary>
    /// Модель стороны акта
    /// </summary>
    public class DocumentApiModel : DocumentBaseApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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