namespace DocumentGenerator.Web.Models.Product
{
    /// <summary>
    /// Модель товара
    /// </summary>
    public class ProductApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Актуальная цена
        /// </summary>
        public decimal Cost { get; set; }
    }
}