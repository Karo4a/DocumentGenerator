namespace DocumentGenerator.Services.Contracts.Models.Product
{
    /// <summary>
    /// Модель создания товара
    /// </summary>
    public class ProductCreateModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Актуальная цена
        /// </summary>
        public decimal Cost { get; set; }
    };
}
