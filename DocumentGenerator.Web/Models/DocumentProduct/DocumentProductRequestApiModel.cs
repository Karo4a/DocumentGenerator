namespace DocumentGenerator.Web.Models.DocumentProduct
{
    /// <summary>
    /// Модель редактирования документа
    /// </summary>
    public class DocumentProductRequestApiModel : DocumentProductBaseApiModel
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }
    }
}