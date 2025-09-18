namespace DocumentGenerator.Web.Models.Product
{
    /// <summary>
    /// Модель товара
    /// </summary>
    public class ProductApiModel : ProductRequestApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}